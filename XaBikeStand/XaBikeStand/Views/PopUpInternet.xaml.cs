using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace XaBikeStand.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpInternet : PopupPage
    {
        public PopUpInternet()
        {
            InitializeComponent();


        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}