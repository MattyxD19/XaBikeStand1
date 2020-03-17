using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XaBikeStand.Models;
using XaBikeStand.Services;
using XaBikeStand.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace XaBikeStand.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {

        /**
         * Breaking the MVVM pattern since it was hard to bind the map
         * -Mathias
         */

        private MapViewModel ViewModel => BindingContext as MapViewModel;

        public MapView()
        {
            InitializeComponent();
            Mapcontent();
            BindingContext = new MapViewModel();
            
            

            //foreach (var pin in listPins)
            //{
            //    AppMap.Pins.Add(pin);
            //}
            //Content = AppMap;
            
        }       


        public void Mapcontent()
        {
            Position position = new Position(54.912794, 9.779231);
            MapSpan startSpan = new MapSpan(position, 0.01, 0.01);
            //AppMap.MoveToRegion(startSpan);
        }


    }
}