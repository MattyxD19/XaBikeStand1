using System;
using System.Collections.Generic;
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



        private String username;

        public String Username
        {
            get { return username; }
            set { username = value;  }
        }


        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
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
            }

        }

    }
}
