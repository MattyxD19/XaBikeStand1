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
    class AccountViewModel : BaseViewModel
    {
        private User updatedUser;


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
                propertyIsChanged();
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
                propertyIsChanged();
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

                propertyIsChanged();

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

                propertyIsChanged();

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

                propertyIsChanged();

            }
        }

        #endregion

        public Command ChangeInfoCMD => new Command(async () =>
        {
            //Just testing
            updatedUser.userName = "Mathias";
            updatedUser.email = "Mathias@Test.dk";
            updatedUser.psw = "Test";
        

            if (EnableSaveButton == false)
            {
                EnableSaveButton = true;
                VisiblePassword = false;
            }
        });

        public Command SaveInfoCMD => new Command(async () =>
        {
            updatedUser.userName = AccountUserName;
            updatedUser.psw = AccountPassword;
            updatedUser.email = AccountEmail;


            if (EnableSaveButton == true)
            {
                EnableSaveButton = false;
                VisiblePassword = true;
            }

            //UpdateUser();

        });


     




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
