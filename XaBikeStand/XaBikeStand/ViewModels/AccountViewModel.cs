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
    class AccountViewModel : BaseViewModel, INotifyPropertyChanged
    {

        /**
         * A user should follow from the login page
         */

        //public AccountViewModel(User user)
        //{
        //    user.userName = _AccountUserName;
        //    user.psw = _AccountPassword;
        //    user.email = _AccountEmail;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _VisiblePassword;

        public bool VisiblePassword
        {
            get 
            {
                return _VisiblePassword; 
            }
            set 
            { 
               _VisiblePassword = value;
               OnPropertyChanged();
            }
            
        }

        

        private bool _EnableSaveButton;

        public bool EnableSaveButton
        {
            get 
            { 
                return _EnableSaveButton; 
            }
            set 
            { 
                _EnableSaveButton = value;
                OnPropertyChanged();
            }
        }




        private string _AccountUserName;

        public string AccountUserName
        {
            get
            {
                return _AccountUserName;
            }

            set
            {
                _AccountUserName = value;

                OnPropertyChanged();
       
            }
        }

        private string _AccountPassword;

        public string AccountPassword
        {
            get
            {
                return _AccountPassword;
            }

            set
            {
                _AccountPassword = value;

                OnPropertyChanged();

            }
        }

        private string _AccountEmail;
        public string AccountEmail
        {
            get
            {
                return _AccountEmail;
            }

            set
            {
                _AccountEmail = value;

                OnPropertyChanged();

            }
        }

     
       

        public Command ChangeInfoCMD => new Command(async () =>
        {
            //Just testing
            AccountUserName = "Mathias";
            AccountEmail = "Mathias@Test.dk";
            AccountPassword = "Test";
        

            if (EnableSaveButton == false)
            {
                EnableSaveButton = true;
                VisiblePassword = false;
            }
        });

        public Command SaveInfoCMD => new Command(async () =>
        {
            _AccountUserName = AccountUserName;
            _AccountPassword = AccountPassword;
            _AccountEmail = AccountEmail;

            //Testing
            Console.WriteLine(AccountUserName);
            Console.WriteLine(AccountEmail);
            Console.WriteLine(AccountPassword);
            Console.WriteLine("Save info CMD works!");

            if (EnableSaveButton == true)
            {
                EnableSaveButton = false;
                VisiblePassword = true;
            }
        });

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /***
         * The updated user should be sent to the database
         * the method should be called from the SaveInfoCMD
         * when done
         */

        //private void UpdateUser(User user)
        //{
            
        //    user.userName = _AccountUserName;
        //    user.psw = _AccountPassword;
        //    user.email = _AccountEmail;
        //}

    }
}
