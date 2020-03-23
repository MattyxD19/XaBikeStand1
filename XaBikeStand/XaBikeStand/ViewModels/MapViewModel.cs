using System;
using System.Collections.ObjectModel;
using XaBikeStand.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel
    {


        ServerClient serverClient = new ServerClient();
        public ObservableCollection<BikeStation> bikeStations;

        #region --Binding properties--

        private ObservableCollection<Pin> pins { get; set; }

        public ObservableCollection<Pin> Pins
        {
            get { return pins; }
            set { pins = value; propertyIsChanged(); }
        }
        #endregion

        public MapViewModel()
        {
            try
            {
                GetPins();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Loads all the bikestations from the back-end on startup
        /// The bikestation information is then added to the pin
        /// where as the pin is added to the map
        /// </summary>
        private void GetPins()
        {
            Pins = new ObservableCollection<Pin>();
            bikeStations = serverClient.GetBikeStations();

            foreach (var item in bikeStations)
            {
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

        /// <summary>
        /// A custom eventhandler
        /// It shows information about a bikestation</summary>
        /// when the pin is tapped on the map 
        /// <param name="tappedPin"></param>
        public async void OnPinClicked(Pin tappedPin)
        {
            var availablespots = serverClient.GetAvailability(tappedPin.Label.Substring(4, tappedPin.Label.Length - 4));

            await Application.Current.MainPage.DisplayAlert("Location: " + tappedPin.Address, "Spaces: " + availablespots.Total + "\n" + "Occupied: " + availablespots.Occupied, "OK");
        }
    }
}