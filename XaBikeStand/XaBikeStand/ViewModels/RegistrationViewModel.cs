using System;
using System.Collections.Generic;
using System.Text;
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
        private String email;


        private bool userNameErrorVisibile;

        public bool UserNameErrorVisibile
        {
            get { return userNameErrorVisibile; }
            set { userNameErrorVisibile = value; propertyIsChanged(); }
        }

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

        public bool IsEmailValid { get; set; }

        public ICommand RegisterAccountCommand { get; set; }

        public ICommand UsernameOnFocusCommand { get; set; }

        public ICommand GoToLoginCommand {get; set;}
        public RegistrationViewModel ()
        {
            sharedData = SingletonSharedData.GetInstance();
            serverClient = new ServerClient();
            RegisterAccountCommand = new Command(RegisterAccount);
            UsernameOnFocusCommand = new Command(UsernameOnFocus);
            GoToLoginCommand = new Command(GoToLogin);
            IsEmailValid = false;
        }
        private async void GoToLogin()
        {
            await NavigationService.NavigateToAsync(typeof(LoginViewModel));
        }

        private void UsernameOnFocus()
        {
            UserNameErrorVisibile = false;
        }

        private async void RegisterAccount()
        {

            //bool isEmailValid = Regex.IsMatch(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            //@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", email ,RegexOptions.IgnoreCase);

            bool isEmailValid = (Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            bool isPasswordValid = (Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            //bool isPasswordValid = Regex.IsMatch(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", password, RegexOptions.IgnoreCase);
            Console.WriteLine("email " + isEmailValid + "test " + password);
            if (isEmailValid && isPasswordValid && !String.IsNullOrEmpty(username))
            {
                Console.WriteLine("email valid" + isEmailValid);
                User user = new User { userName = username, psw = password, email = email };
                ISerializable serializable = serverClient.PostData(user, Target.RegiserUser);
                if (serializable != null)
                {
                    sharedData.LoggedInUser = (User)serializable;
                    await NavigationService.NavigateToAsync(typeof(ActionsViewModel));
                } else
                {
                    UserNameErrorVisibile = true;
                }
            }
        }
    }
}
