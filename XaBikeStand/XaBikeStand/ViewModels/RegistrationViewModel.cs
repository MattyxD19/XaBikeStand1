﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private ServerClient serverClient;

        private SingletonSharedData sharedData;

        #region --Binding properties--
        private String email;
        public String Email
        {
            get { return email; }
            set { email = value; }
        }


        private bool userNameErrorVisibile;

        public bool UserNameErrorVisibile
        {
            get { return userNameErrorVisibile; }
            set { userNameErrorVisibile = value; propertyIsChanged(); }
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
            set { password = value; }
        }
        #endregion

        #region --Commands--
        public ICommand RegisterAccountCommand { get; set; }

        public ICommand UsernameOnFocusCommand { get; set; }

        public ICommand GoToLoginCommand { get; set; }
        #endregion

        public RegistrationViewModel()
        {
            sharedData = SingletonSharedData.GetInstance();
            serverClient = new ServerClient();
            RegisterAccountCommand = new Command(RegisterAccount);
            UsernameOnFocusCommand = new Command(UsernameOnFocus);
            GoToLoginCommand = new Command(GoToLogin);
        }

        /// <summary>
        /// Navigates to the login view
        /// </summary>
        private async void GoToLogin()
        {
            await NavigationService.NavigateToAsync(typeof(LoginViewModel));
        }

        private void UsernameOnFocus()
        {
            UserNameErrorVisibile = false;
        }

        /// <summary>
        /// Registers an account and goes to the actions view if successful
        /// </summary>
        private async void RegisterAccount()
        {
            bool isEmailValid = (Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            bool isPasswordValid = (Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            if (isEmailValid && isPasswordValid && !String.IsNullOrEmpty(username))
            {
                User user = new User { userName = username, psw = password, email = email };
                User addedUser = serverClient.AddUser(user);
                if (addedUser != null)
                {
                    sharedData.LoggedInUser = addedUser;
                    ((MasterDetailPage)Application.Current.MainPage).IsGestureEnabled = true;
                    await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
                }
                else
                {
                    UserNameErrorVisibile = true;
                }
            }
        }
    }
}
