using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class ActionsViewModel : BaseViewModel
    {
        SingletonSharedData sharedData;

        private bool isLockIDEnabled;

        public bool IsLockIDEnabled
        {
            get { return isLockIDEnabled; }
            set { isLockIDEnabled = value; propertyIsChanged(); }
        }

        public ActionsViewModel()
        {
            sharedData = SingletonSharedData.GetInstance();
            IsLockIDEnabled = true;
            FriendEntryUnfocused = new Command(FriendEntryValidation);
            StationEntryUnfocused = new Command(StationEntryValidation);
            LockEntryUnfocused = new Command(LockEntryValidation);
            LockIDFocusedCommand = new Command(LockIDFocused);
            NavigateScannerViewCommand = new Command(NavigateScannerView);
           
            AddFriend = false;
            serverClient = new ServerClient();
            LockVisible = true;
        }

        ServerClient serverClient;

        public bool stationIDEntered = false;
        public bool lockIDEntered = false;

        /**
         * A custom behavior has been added to check if there is data
         * in the entries, they are updated if the user is not focused on the entries
         */
        public ICommand FriendEntryUnfocused { get; protected set; }
        public ICommand StationEntryUnfocused { get; protected set; }
        public ICommand LockEntryUnfocused { get; protected set; }

        public ICommand NavigateScannerViewCommand { get; set; }

       

        private string _FriendEmail;

        public string FriendEmail
        {
            get 
            { 
                return _FriendEmail;
            }
            set 
            { 
                _FriendEmail = value;
                propertyIsChanged();
            }
        }


        private string _StationID;

        public string StationID
        {
            get
            { 
                return _StationID; 
            }
            set 
            { 
                _StationID = value;
                propertyIsChanged();
            }
        }

        private string _LockID;

        public string LockID
        {
            get
            {
                return _LockID;
            }
            set
            {
                _LockID = value;
                propertyIsChanged();
            }
        }


        private bool _UnlockVisible;

        public bool UnlockVisible
        {
            get 
            { 
                return _UnlockVisible; 
            }
            set 
            { 
                _UnlockVisible = value;
                propertyIsChanged();
            }
        }

        private bool isLockErrorVisible;

        public bool IsLockErrorVisible
        {
            get
            {
                return isLockErrorVisible;
            }
            set
            {
                isLockErrorVisible = value;
                propertyIsChanged();
            }
        }

        private bool isUnlockErrorVisible;

        public bool IsUnlockErrorVisible
        {
            get
            {
                return isUnlockErrorVisible;
            }
            set
            {
                isUnlockErrorVisible = value;
                propertyIsChanged();
            }
        }



        private bool _LockVisible;

        public bool LockVisible
        {
            get
            {
                return _LockVisible;
            }
            set
            {
                _LockVisible = value;
                propertyIsChanged();
            }
        }

        private bool _AddFriend;

        public bool AddFriend
        {
            get
            {
                return _AddFriend;
            }
            set
            {
                _AddFriend = value;
                propertyIsChanged();
            }
        }

        #region --Command implementations--

        public Command UnlockCMD => new Command(async () =>
        {
            bool succes = serverClient.Unlock();

            if (succes)
            {
                IsUnlockErrorVisible = false;
                IsLockIDEnabled = true;
                LockVisible = true;
                UnlockVisible = false;
            } else
            {
                IsUnlockErrorVisible = true;
            }


            
        });

        public Command LockCMD => new Command(async () =>
        {
            bool succes = false;
            if (int.TryParse(LockID, out int convertedLockID))
            {
               succes = serverClient.Lock(convertedLockID);
            } else
            {
                IsLockErrorVisible = true;
            }
            Console.WriteLine(  "this worked " + succes);
            if (succes)
            {
                IsLockIDEnabled = false;
                LockVisible = false;
                UnlockVisible = true;
            } else
            {
                IsLockErrorVisible = true;
            }
            
        });
        public ICommand LockIDFocusedCommand { get; set; }
        #endregion



        /**
         * The following three methods are used for entry validation
         * they are currently only checking for any value in the entries
         * but they should preferably check for valid data in the back-end
         */

        private void FriendEntryValidation(object FriendEntry)
        {
            AddFriend = true;
            FriendEmail = _FriendEmail;
        }
        private void StationEntryValidation(object FriendEntry)
        {
            stationIDEntered = true;
            StationID = _StationID;
            Console.WriteLine(_StationID);
        }
        private void LockEntryValidation(object FriendEntry)
        {
            lockIDEntered = true;
            LockID = _LockID;
            Console.WriteLine(_LockID);
        }

        private void LockIDFocused()
        {
            IsLockErrorVisible = false;
        }

        private async void NavigateScannerView()
        {
            await NavigationService.NavigateToAsync(typeof(ScannerViewModel));
        }

        public void OnAppearing()
        {
            if (sharedData.ScannedBikestandID != null)
            {
                LockID = sharedData.ScannedBikestandID;
            }
            sharedData.ScannedBikestandID = null;

            int lockedBikestandID;

            if (int.TryParse(serverClient.GetLockedBikestand(), out lockedBikestandID))
            {
                LockID = "" + lockedBikestandID;
                LockVisible = false;
                UnlockVisible = true;
                IsLockIDEnabled = false;
            }
        }

    }
}
