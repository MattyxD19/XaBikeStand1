using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class AccountViewModel : BaseViewModel
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

        #region -- Commands--
        public ICommand UpdateAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        #endregion

        public AccountViewModel()
        {
            sharedData = SingletonSharedData.GetInstance();
            serverClient = new ServerClient();
            UpdateAccountCommand = new Command(UpdateAccount);
            DeleteAccountCommand = new Command(DeleteAccount);
            Username = "Username : " + sharedData.LoggedInUser.userName;
            Email = sharedData.LoggedInUser.email;
        }


        /// <summary>
        /// Deletes an account
        /// </summary>
        private async void DeleteAccount()
        {
            bool deleteValidation = await Application.Current.MainPage.DisplayAlert("Delete your account", "Are you sure", "Delete", "Cancel");
            if (deleteValidation)
            {
                if (serverClient.DeleteUser(sharedData.LoggedInUser.userName) != null)
                {

                    Application.Current.Properties.Remove("username");
                    NavigationService.NavigateToAsync(typeof(LoginViewModel));
                }
            }
        }

        /// <summary>
        /// Updates an account
        /// </summary>
        private async void UpdateAccount()
        {
            bool isEmailValid = (Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            bool isPasswordValid = false;
            if (!String.IsNullOrEmpty(password))
            {
                isPasswordValid = (Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            }
            if (isEmailValid && (isPasswordValid || String.IsNullOrEmpty(password)) && !String.IsNullOrEmpty(username))
            {
                User user = null;
                if (String.IsNullOrEmpty(password))
                {
                    user = new User { userName = sharedData.LoggedInUser.userName, email = email };
                }
                else
                {
                    user = new User { userName = sharedData.LoggedInUser.userName, psw = password, email = email };
                }
                User updatedUser = serverClient.UpdateUser(user);
                if (updatedUser != null)
                {
                    sharedData.LoggedInUser.email = updatedUser.email;

                    await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
                }
            }
        }
    }
}
