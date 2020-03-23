using XaBikeStand.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

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

        }

        private void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            Pin selectedPin = (Pin)sender;
            ((MapViewModel)this.BindingContext).OnPinClicked(selectedPin);
        }
    }
}