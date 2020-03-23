﻿using Newtonsoft.Json;
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
            try
            {
                GetPins();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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

        /// <summary>
        /// Loads all the bikestations from the back-end on startup
        /// The bikestation information is then added to the pin
        /// where as the pin is added to the map
        /// </summary>
        
        private void GetPins()
        {
            Pins = new ObservableCollection<Pin>();
            var bikeStations = serverClient.GetBikeStations();

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
            var availablespots = serverClient.GetAvailability(tappedPin.Label);
            await Application.Current.MainPage.DisplayAlert("Pladser ved: " + tappedPin.Address, "Antal pladser: " + availablespots.Total + "\n" + "Optaget: " + availablespots.Occupied, "OK");

        }
    }
}