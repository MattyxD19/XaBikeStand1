using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XaBikeStand.Services;
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
            Mapcontent();
            Content = AppMap;
        }

        Pin alsionPin = new Pin
        {
            Label = "Alsion",
            Address = "Alsion",
            Type = PinType.Place,
            Position = new Position(54.912794, 9.779231)
        };

        Pin FitnessWorldPin = new Pin
        {
            Label = "Fitness World",
            Address = "Fitness World",
            Type = PinType.Place,
            Position = new Position(54.919936, 9.807708)
        };

        Map AppMap = new Map
        {
            MapType = MapType.Street,
            IsShowingUser = true,
            
        };

        public void Mapcontent()
        {
            Position position = new Position(54.912794, 9.779231);
            MapSpan startSpan = new MapSpan(position, 0.01, 0.01);
            AppMap.MoveToRegion(startSpan);

            AppMap.Pins.Add(alsionPin);
            AppMap.Pins.Add(FitnessWorldPin);

            
            
        }


    }
}