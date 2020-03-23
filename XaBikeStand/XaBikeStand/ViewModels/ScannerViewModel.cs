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

        public ICommand GoBackCommand { get; set; }
        #endregion

        public ScannerViewModel()
        {
            OnScanCommand = new Command(OnScan);
            GoBackCommand = new Command(GoBack);
        }

        /// <summary>
        /// Navigates to the actionsview
        /// </summary>
        private async void GoBack()
        {
            await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
        }

        /// <summary>
        /// Gets the result of the scan and saves it to the SharedData class
        /// </summary>
        private async void OnScan()
        {
            SingletonSharedData.GetInstance().ScannedBikestandID = result.Text;
            await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
        }
    }
}
