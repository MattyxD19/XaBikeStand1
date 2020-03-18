using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public MapViewModel()
        {

            PinClicked = new Command(OnPinClicked);
            GetPins();
        }

       
        public ICommand PinClicked { get; protected set; }


        public ICommand TestCommand { get; protected set; }



        private Pin _pin;

        public event PropertyChangedEventHandler PropertyChanged;

        ServerClient serverClient = new ServerClient();
        public ObservableCollection<BikeStation> bikeStations = new ObservableCollection<BikeStation>();


        #region --Binding--

        private ObservableCollection<Pin> pins { get; set; }


        public ObservableCollection<Pin> Pins
        {
            get { return pins; }
            set { pins = value; OnPropertyChanged(); }
        }

        public Pin Pin
        {
            get => _pin;
            set => _pin = value;
        }
        #endregion



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private void GetPins()
        {
            Pins = new ObservableCollection<Pin>();
            var bikeStations = serverClient.GetBikeStations();


            foreach (var item in bikeStations)
            {
                Console.WriteLine("Latitude: " + item.latitude);
                Console.WriteLine("Longitude: " + item.longtitude);
                Pin pin = new Pin
                {

                    Label = "ID: " + item.bikeStationID,
                    Address = item.title,
                    Type = PinType.Place,
                    Position = new Position(item.latitude, item.longtitude),

                };
                Pins.Add(pin);

            }
        }


        Page page = new Page();
        public void OnPinClicked()
        {
            Pin.MarkerClicked += async (sender, args) =>
            {

                var availablespots = serverClient.GetAvailability(Pin.Label);
                await page.DisplayAlert("Pladser ved: " + Pin.Address, "Antal pladser: " + availablespots.Total + "\n" + "Optaget: " + availablespots.Occupied, "OK");
            };
        }
    }
}