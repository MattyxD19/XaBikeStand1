using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XaBikeStand.Models;
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

        public MapView()
        {
            InitializeComponent();
            //Mapcontent();
            //Content = AppMap;
        }

        //Map AppMap = new Map
        //{
        //    IsShowingUser = true,
            
            
        //};

        //public void Mapcontent()
        //{
        //    Position position = new Position(54.912794, 9.779231);
        //    MapSpan startSpan = new MapSpan(position, 0.01, 0.01);
        //    AppMap.MoveToRegion(startSpan);
        //}
    }
}