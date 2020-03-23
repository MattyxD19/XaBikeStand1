using System;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class LoginViewModel : BaseViewModel
    {

        private ServerClient serverClient;
        private SingletonSharedData sharedData;


        #region --Binding properties--
        private bool isLoginErrorVisible;

        public bool IsLoginErrorVisible
        {
            get { return isLoginErrorVisible; }
            set { isLoginErrorVisible = value; propertyIsChanged(); }
        }

        private String username;

        public String Username
        {
            get { return username; }
            set { username = value; }
        }


        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; propertyIsChanged(); }
        }
        #endregion

        #region --Commands--
        public ICommand LoginCommand { get; set; }

        public ICommand GoToRegisterPageCommand { get; set; }

        public ICommand OnEntryFocusedCommand { get; set; }
        #endregion


        public LoginViewModel()
        {
            ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = false;
            if (username != null)
            {
                Username = username;
            }
            if (Application.Current.Properties.ContainsKey("username"))
            {
                Username = Application.Current.Properties["username"].ToString();
            }
            LoginCommand = new Command(Login);
            GoToRegisterPageCommand = new Command(GoToRegisterPage);
            OnEntryFocusedCommand = new Command(OnEntryFocused);
            serverClient = new ServerClient();
            sharedData = SingletonSharedData.GetInstance();
        }


        private void OnEntryFocused()
        {
            IsLoginErrorVisible = false;
        }

        /// <summary>
        /// Changes the view if the login is successful
        /// </summary>
        private async void Login()
        {
            User user = serverClient.Login(username, password);
            if (user != null)
            {
                sharedData.LoggedInUser = user;
                ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = true;
                SaveUsername(user.userName);
                await NavigationService.NavigateToAsync(typeof(ActionsViewModel));

            }
            else
            {
                IsLoginErrorVisible = true;
                Password = "";
            }
        }

        /// <summary>
        /// Saves the username in the app 
        /// </summary>
        /// <param name="username"></param>
        private async void SaveUsername(String username)
        {
            if (!Application.Current.Properties.ContainsKey("username"))
            {
                Application.Current.Properties.Add("username", username);
            }
            else
            {
                Application.Current.Properties["username"] = username;
            }
            await Application.Current.SavePropertiesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Navigates to the registration page
        /// </summary>
        private async void GoToRegisterPage()
        {
            await NavigationService.NavigateToAsync(typeof(RegistrationViewModel));
        }
    }
}
