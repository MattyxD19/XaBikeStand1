﻿using System;
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
   
        public ICommand OnEntryFocusedCommand { get; set; }

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

        public LoginViewModel(string username)
        {
            ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = false;
            if (username != null)
            {
                Username = username;
            }
            LoginCommand = new Command(Login);
            GoToRegisterPageCommand = new Command(GoToRegisterPage);
            OnEntryFocusedCommand = new Command(OnEntryFocused);
            serverClient = new ServerClient();
            sharedData = SingletonSharedData.GetInstance();

        }

        private void OnEntryFocused ()
        {
            IsLoginErrorVisible = false;
        }

        private async void Login()
        {
            User user = serverClient.Login(username, password);
            if (user != null)
            {
                sharedData.LoggedInUser = user;
                ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = true;
                await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
            }
            else
            {
                IsLoginErrorVisible = true;
                Password = "";
            }
        }

        private async void GoToRegisterPage()
        {
            await NavigationService.NavigateToAsync(typeof(RegistrationViewModel));
        }


    }
}
