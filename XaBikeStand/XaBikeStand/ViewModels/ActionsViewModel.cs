using System;
using System.Windows.Input;
using XaBikeStand.Models;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class ActionsViewModel : BaseViewModel
    {
        SingletonSharedData sharedData;
        ServerClient serverClient;
        private string sharedAccessTo;



        #region -- Binding properties -- 
        private bool isLockIDEnabled;

        public bool IsLockIDEnabled
        {
            get { return isLockIDEnabled; }
            set { isLockIDEnabled = value; propertyIsChanged(); }
        }



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

        private string lockID;

        public string LockID
        {
            get
            {
                return lockID;
            }
            set
            {
                lockID = value;
                propertyIsChanged();
            }
        }


        private bool unlockVisible;

        public bool UnlockVisible
        {
            get
            {
                return unlockVisible;
            }
            set
            {
                unlockVisible = value;
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


        private bool lockVisible;

        public bool LockVisible
        {
            get
            {
                return lockVisible;
            }
            set
            {
                lockVisible = value;
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
        #endregion

        #region --Commands--

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

        /// <summary>
        /// Removes the shared access to a bikestand
        /// </summary>
        private void RemoveShared()
        {
            if (serverClient.DeleteSharedAccess(sharedAccessTo))
            {
                IsAddFriendEnabled = true;
                IsShared = false;
            }
        }

        /// <summary>
        /// Shares the access to a bikestand with another user
        /// </summary>
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

        /// <summary>
        /// Locks a bikestand and updates the interface
        /// </summary>
        private void Lock()
        {
            BikeStandRegistration bikestandRegistration = null;
            if (int.TryParse(LockID, out int convertedLockID))
            {
                bikestandRegistration = serverClient.Lock(convertedLockID);
            }
            else
            {
                IsLockErrorVisible = true;
            }
            if (bikestandRegistration != null)
            {
                BikeStation bikeStation = serverClient.GetBikeStation("" + bikestandRegistration.BikeStandID);
                IsAddFriendEnabled = true;
                IsLockIDEnabled = false;
                LockVisible = false;
                UnlockVisible = true;
                IsLockErrorVisible = false;
                IsAddFriendVisible = true;
                BikeStationText = String.Format("Your bike was locked at {0} on the {1}", bikeStation.title, bikestandRegistration.RegistrationTime);
                IsBikeStationVisible = true;
            }
            else
            {
                IsLockErrorVisible = true;
            }
        }


        /// <summary>
        /// Unlocks the bikestand and updates the interface
        /// </summary>
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

        /// <summary>
        /// Navigates to the scanner view
        /// </summary>
        private async void NavigateScannerView()
        {
            await NavigationService.NavigateToAsync(typeof(ScannerViewModel));
        }

        /// <summary>
        /// This method updates the interface according to the relevant data from the backend when the Actionview appears
        /// </summary>
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
            BikeStandRegistration bikestandRegistration = serverClient.GetLockedBikestand();
            if (bikestandRegistration != null)
            {
                LockID = "" + bikestandRegistration.BikeStandID;
                LockVisible = false;
                UnlockVisible = true;
                IsLockIDEnabled = false;
                IsAddFriendEnabled = true;
                IsAddFriendVisible = true;
                BikeStation bikeStation = serverClient.GetBikeStation("" + bikestandRegistration.BikeStandID);
                if (bikeStation != null)
                {
                    BikeStationText = String.Format("Your bike was locked at {0} on the {1}", bikeStation.title, bikestandRegistration.RegistrationTime);
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
