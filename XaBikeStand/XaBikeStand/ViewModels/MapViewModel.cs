using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<Pin> Locations { get; set; }

        public MapViewModel()
        {
            Locations = new List<Pin>();

            //_Pin. = "bikeStation.Address";
            //_Pin.Address = "Cykler fri: " + "bikeStation.Bikes";
            //_Pin.Type = PinType.Place;
            //_Pin.Position = new Position(54.913197, 9.778213);

            //Locations.Add(_Pin);

            //pinCollection.Add(new Pin { Label = "bikeStation.Address", 
            //                            Address = "Cykler fri: " + "bikeStation.Bikes", 
            //                            Type = PinType.Place, 
            //                            Position = new Position(54.913197, 9.778213) });

            //AddPins();
        }






        #region --Binding--
        private ObservableCollection<Pin> _Pins = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> Pins
        {
            get { return _Pins; }
            set
            {
                _Pins = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Monkeys"));
                }
            }
        }



        private Position _Position;

        public Position Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged(); }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }

        private string _Label;

        public string Label
        {
            get { return _Label; }
            set { _Label = value; OnPropertyChanged(); }
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

        private void LoadPins()
        {
            Pins.Add(new Pin
            {
                Label = "bikeStation.Address",
                Address = "Cykler fri: " + "bikeStation.Bikes",
                Type = PinType.Place,
                Position = new Position(54.913197, 9.778213)
            });
        }

            //private void AddPins()
            //{
            //    _Locations.Label = "bikeStation.Address";
            //    _Locations.Address = "Cykler fri: " + "bikeStation.Bikes";
            //    _Locations.Type = PinType.Place;
            //    _Locations.Position = new Position(54.913197, 9.778213);

            //    pinCollection.Add(_Locations);

            //    _Locations.Label = "bikeStation.Address";
            //    _Locations.Address = "Cykler fri: " + "bikeStation.Bikes";
            //    _Locations.Type = PinType.Place;
            //    _Locations.Position = new Position(54.919999, 9.807773);

            //    pinCollection.Add(_Locations);
            //}

        }
}
