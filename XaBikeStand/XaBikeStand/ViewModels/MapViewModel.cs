using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel, INotifyPropertyChanged
    {       

        public event PropertyChangedEventHandler PropertyChanged;

       

        private double _Longitude;

        public double Longitude
        {
            get 
            { 
                return _Longitude; 
            }
            set 
            {
                _Longitude = value;
                OnPropertyChanged();
            }
        }

        private double _Latitude;

        public double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
                OnPropertyChanged();
            }
        }


        //private void  GetPosition()
        //{
        //    try
        //    {
        //        var location = Geolocation.GetLastKnownLocationAsync();

        //        if (location != null)
        //        {
        //            Console.WriteLine("Unlock CMD works!");
        //            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
        //        }
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        Console.WriteLine("Handle not supported on device exception");
        //    }
        //}

        public double Loc { get; set; }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
