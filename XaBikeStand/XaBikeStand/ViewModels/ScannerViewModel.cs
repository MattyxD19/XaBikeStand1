using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {

        private ZXing.Result result;

        public ZXing.Result Result
        {
            get { return result; }
            set { result = value; }
        }

        public ICommand OnScanCommand { get; set; }

        public ScannerViewModel()
        {
            OnScanCommand = new Command(OnScan);
        }

        private async void OnScan()
        {
            SingletonSharedData.GetInstance().ScannedBikestandID = result.Text;
            await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
        }
    }
}
