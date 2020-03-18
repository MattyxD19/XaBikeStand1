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
            //GetPins();
            PinClicked = new Command(OnPinClicked);
        }

        
        public ICommand PinClicked { get; protected set; }

        private Pin _pin;

        public event PropertyChangedEventHandler PropertyChanged;

        ServerClient serverClient = new ServerClient();
        public ObservableCollection<BikeStation> bikeStations = new ObservableCollection<BikeStation>();


        #region --Binding--

        public ObservableCollection<Pin> Pins { get; set; }

        public Pin Pin
        {
            get  => _pin; 
            set  => _pin = value; 
        }
        #endregion

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void GetPins()
        {
            var getStations = serverClient.GetBikeStations();
            int count = 0;
            foreach (var station in getStations)
            {
                bikeStations.Add(station);
                count++;
                Console.WriteLine("Stations: " + count);
            }

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

                //Pins.Add(pin);
                
                //_Pins.MarkerClicked += async (sender, args) => {
                //    var availablespots = serverClient.GetAvailability(_Pins.Label);
                //    await DisplayAlert("Pladser ved: " + _Pins.Address, "Antal pladser: " + availablespots.Total + "\n" + "Optaget: " + availablespots.Occupied, "OK");
                //};

            }
        }

        private void OnPinClicked(object pin)
        {
            Console.WriteLine("Pin clicked: " + _pin.Label);
        }

    }
}