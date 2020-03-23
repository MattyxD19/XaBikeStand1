using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        #region --Binding properties--
        private ZXing.Result result;

        public ZXing.Result Result
        {
            get { return result; }
            set { result = value; }
        }
        #endregion

        #region --Commands--
        public ICommand OnScanCommand { get; set; }
        #endregion

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
