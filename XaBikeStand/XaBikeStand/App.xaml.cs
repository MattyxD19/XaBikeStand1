using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using XaBikeStand.Services;
using XaBikeStand.ViewModels;
using XaBikeStand.Views;
using Xamarin.Forms;

namespace XaBikeStand
{
    public partial class App : Application
    {

        ISettingsService _settingsService;
        public App()
        {
            InitializeComponent();



            ServiceContainer.Register<ISettingsService>(() => new SettingsService());
            _settingsService = ServiceContainer.Resolve<ISettingsService>();
            ServiceContainer.Register<INavigationService>(() => new NavigationService(_settingsService));
            ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
            ServiceContainer.Register<RegistrationViewModel>(() => new RegistrationViewModel());
            ServiceContainer.Register<ActionsViewModel>(() => new ActionsViewModel());
            ServiceContainer.Register<MapViewModel>(() => new MapViewModel());
            ServiceContainer.Register<AccountViewModel>(() => new AccountViewModel());
            ServiceContainer.Register<ScannerViewModel>(() => new ScannerViewModel());


            var masterDetailViewModel = new MasterDetailViewModel();
            ServiceContainer.Register<MasterDetailViewModel>(() => masterDetailViewModel);

            var master = new MasterDetail();
            MainPage = master;
            master.BindingContext = masterDetailViewModel;

            CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;

        }

        private async void OnConnectivityChanged(Object sender, ConnectivityChangedEventArgs args)
        {
            if (!args.IsConnected)
            {
                await PopupNavigation.PushAsync(new PopUpInternet());
            }
            else
            {
                await PopupNavigation.PopAllAsync();
            }
        }


        private Task InitNavigation()
        {
            var navigationService = ServiceContainer.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected async override void OnStart()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await PopupNavigation.PushAsync(new PopUpInternet());
            }
            // Handle when your app starts
            base.OnStart();
            await InitNavigation();
            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
