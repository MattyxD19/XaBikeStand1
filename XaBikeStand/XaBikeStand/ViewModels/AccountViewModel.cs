using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class AccountViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private User updatedUser;

        public event PropertyChangedEventHandler PropertyChanged;

        #region --Bindings--

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

        #endregion


        #region --User properties--

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

        #endregion

        public Command ChangeInfoCMD => new Command(async () =>
        {
            //Just testing
            updatedUser.UserName = "Mathias";
            updatedUser.Email = "Mathias@Test.dk";
            updatedUser.Psw = "Test";
        

            if (EnableSaveButton == false)
            {
                EnableSaveButton = true;
                VisiblePassword = false;
            }
        });

        public Command SaveInfoCMD => new Command(async () =>
        {
            updatedUser.UserName = AccountUserName;
            updatedUser.Psw = AccountPassword;
            updatedUser.Email = AccountEmail;


            if (EnableSaveButton == true)
            {
                EnableSaveButton = false;
                VisiblePassword = true;
            }

            //UpdateUser();

        });

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /***
         * The updated user should be sent to the database
         * the method should be called from the SaveInfoCMD
         * when done
         *

        private void UpdateUser()
        {
            //Backend 
            //var uriDB = "";
            //var updateContent = JsonConvert.SerializeObject(updatedUser);
            //var response = await client.PutAsync(uriDB, updateContent);
            
        }
        */

    }
}
