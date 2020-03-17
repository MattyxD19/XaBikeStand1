using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel, INotifyPropertyChanged
    {

        BikeStation bikeStation = new BikeStation();
        ServerClient serverClient = new ServerClient();

        public event PropertyChangedEventHandler PropertyChanged;
        public List<Pin> pinCollection = new List<Pin>();

        public MapViewModel()
        {
            AddPins();
        }

        private void AddPins()
        {
            pinCollection.Add(new Pin { Label = "bikeStation.Address", Address = "Cykler fri: " + "bikeStation.Bikes", Type = PinType.Place, Position = new Position(54.913197, 9.778213) });
            pinCollection.Add(new Pin { Label = "bikeStation.Address", Address = "Cykler fri: " + "bikeStation.Bikes", Type = PinType.Place, Position = new Position(54.919999, 9.807773) });

            

        }

        #region --Binding--


        private Pin _pin;

        public Pin pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private void GetBikeStations()
        {
            //serverClient.

            //pinCollection.Add(new Pin { Label = bikeStation.Address, Address = "Cykler fri: " + bikeStation.Bikes, Type = PinType.Place, Position = new Position(bikeStation.Longitude, bikeStation.Latitude) });

        }

        public void PinClicked(object sender, EventArgs e)
        {

        }

        private void PinInfo(object sender, EventArgs e)
        {
            //menu = new PopUpMenu(this, show)
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
