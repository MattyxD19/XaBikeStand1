using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class LoginViewModel : BaseViewModel
    {



        private ServerClient serverClient;
        private SingletonSharedData sharedData;
        public ICommand LoginCommand { get; set; }

        public ICommand GoToRegisterPageCommand { get; set; }


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
            set { password = value;  propertyIsChanged();}
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            GoToRegisterPageCommand = new Command(GoToRegisterPage);
            serverClient = new ServerClient();
            sharedData = SingletonSharedData.GetInstance();

        }
        
        private async void Login ()
        {
            User user = serverClient.Login(username, password);
            if (user != null)
            {
                sharedData.LoggedInUser = user;
                ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = true;
                await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
            } else
            {
                Password = "";
             await Application.Current.MainPage.DisplayAlert("alert", "wrong", "cancel", "okay");
            }
        }

        private async void GoToRegisterPage()
        {
            await NavigationService.NavigateToAsync(typeof(RegistrationViewModel));
        }


    }
}
