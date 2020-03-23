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


        ServerClient serverClient;

        public bool stationIDEntered = false;
        public bool lockIDEntered = false;





        private string shareUsername;

        public string ShareUsername
        {
            get
            {
                return shareUsername;
            }
            set
            {
                shareUsername = value;
                propertyIsChanged();
            }
        }

        private string sharedWithText;

        public string SharedWithText
        {
            get
            {
                return sharedWithText;
            }
            set
            {
                sharedWithText = value;
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

        private string sharedAccessTo; 


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

        private bool isAddFriendEnabled;

        public bool IsAddFriendEnabled
        {
            get
            {
                return isAddFriendEnabled;
            }
            set
            {
                isAddFriendEnabled = value;
                propertyIsChanged();
            }
        }



        private bool isAddFriendVisible;

        public bool IsAddFriendVisible
        {
            get
            {
                return isAddFriendVisible;
            }
            set
            {
                isAddFriendVisible = value;
                propertyIsChanged();
            }
        }

        private bool isBikeStationVisible;

        public bool IsBikeStationVisible
        {
            get
            {
                return isBikeStationVisible;
            }
            set
            {
                isBikeStationVisible = value;
                propertyIsChanged();
            }
        }

        private bool isShared;

        public bool IsShared
        {
            get
            {
                return isShared;
            }
            set
            {
                isShared = value;
                propertyIsChanged();
            }
        }

        


        private string bikeStationText;

        public string BikeStationText
        {
            get { return bikeStationText; }
            set
            {
                bikeStationText = value;
                propertyIsChanged();
            }
        }

        #region --Command implementations--

        public ICommand UnlockCommand { get; set; }

        public ICommand LockCommand { get; set; }

        public ICommand LockIDFocusedCommand { get; set; }

        public ICommand ShareWithFriendCommand { get; set; }
        public ICommand NavigateScannerViewCommand { get; set; }

        public ICommand RemoveSharedCommand { get; set; }

    

        #endregion


        public ActionsViewModel()
        {
            sharedData = SingletonSharedData.GetInstance();
            IsLockIDEnabled = true;

            LockIDFocusedCommand = new Command(LockIDFocused);
            NavigateScannerViewCommand = new Command(NavigateScannerView);
            LockCommand = new Command(Lock);
            UnlockCommand = new Command(Unlock);
            ShareWithFriendCommand = new Command(ShareWithFriend);
            RemoveSharedCommand = new Command(RemoveShared);
            serverClient = new ServerClient();
            LockVisible = true;
        }

        private void RemoveShared()
        {
            Console.WriteLine("sharedaccessto" + sharedAccessTo);
            if (serverClient.DeleteSharedAccess(sharedAccessTo))
            {
                IsAddFriendEnabled = true;
                IsShared = false;
            }
        }

        private void ShareWithFriend()
        {
            if (!String.IsNullOrEmpty(shareUsername))
            {
                if (serverClient.ShareBikestandLock(shareUsername))
                {
                    SharedWithText = "Shared with " + shareUsername;
                    sharedAccessTo = shareUsername;
                    IsShared = true;
                    IsAddFriendEnabled = false;
                    ShareUsername = "";
                }
            }
        }

        private void Lock()
        {
            BikeStation bikeStation = null;
            if (int.TryParse(LockID, out int convertedLockID))
            {
                bikeStation = serverClient.Lock(convertedLockID);
            }
            else
            {
                IsLockErrorVisible = true;
            }
            if (bikeStation != null)
            {
                IsAddFriendEnabled = true;
                IsLockIDEnabled = false;
                LockVisible = false;
                UnlockVisible = true;
                IsLockErrorVisible = false;
                IsAddFriendVisible = true;
                BikeStationText = String.Format("Din cykel blev låst ved {0} d. {1}", bikeStation.title, DateTime.Now);
                IsBikeStationVisible = true;
            }
            else
            {
                IsLockErrorVisible = true;
            }
        }

        private void Unlock()
        {
            bool succes = serverClient.Unlock();

            if (succes)
            {
                IsUnlockErrorVisible = false;
                IsUnlockErrorVisible = false;
                IsLockIDEnabled = true;
                LockVisible = true;
                UnlockVisible = false;
                IsAddFriendVisible = false;
                IsBikeStationVisible = false;
                LockID = "";
                IsShared = false;
            }
            else
            {
                IsUnlockErrorVisible = true;
            }
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
                int qrLockID;
                if (int.TryParse(sharedData.ScannedBikestandID, out qrLockID))
                {
                    LockID = "" + qrLockID;
                }
            }
            sharedData.ScannedBikestandID = null;

            BikeStand lockedBikestand = serverClient.GetLockedBikestand();
            if (lockedBikestand != null)
            {
                LockID = "" + lockedBikestand.bikestandID;
                LockVisible = false;
                UnlockVisible = true;
                IsLockIDEnabled = false;
                IsAddFriendEnabled = true;
                IsAddFriendVisible = true;
                BikeStation bikeStation = serverClient.GetBikeStation(lockedBikestand.bikeStationID);
                if (bikeStation != null)
                {
                    BikeStationText = String.Format("Din cykel blev låst ved {0} d. {1}", bikeStation.title, DateTime.Now);
                    IsBikeStationVisible = true;
                }
                String sharedWithUsername = serverClient.GetSharedUsername();
                if (!String.IsNullOrEmpty(sharedWithUsername))   
                {
                    sharedAccessTo = sharedWithUsername;
                    SharedWithText = "Shared with: " + sharedAccessTo;
                    IsShared = true;
                    IsAddFriendEnabled = false;
                }
            }
            else
            {
                IsAddFriendVisible = false;
                IsBikeStationVisible = false;
                IsShared = false;
                IsAddFriendEnabled = true;
            }
        }
    }
}
