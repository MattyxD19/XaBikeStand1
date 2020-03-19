using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private String  email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private bool isEmailValid;

        public bool IsEmailValid
        {
            get { return isEmailValid; }
            set { isEmailValid = value; }
        }


        private bool isPasswordValid;

        public bool IsPasswordValid
        {
            get { return isPasswordValid; }
            set { isPasswordValid = value; }
        }

        public ICommand RegisterAccountCommand { get; set; }

        public RegistrationViewModel ()
        {
            RegisterAccountCommand = new Command(RegisterAccount);
        }

        private async void RegisterAccount ()
        {
            if (isPasswordValid)
            {
                await NavigationService.NavigateToAsync(typeof(LoginViewModel));
            }

        }

       

    }
}
