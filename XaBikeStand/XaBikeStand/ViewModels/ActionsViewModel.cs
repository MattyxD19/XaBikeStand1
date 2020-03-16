using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class ActionsViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public ActionsViewModel()
        {
            FriendEntryUnfocused = new Command(FriendEntryValidation);
            StationEntryUnfocused = new Command(StationEntryValidation);
            LockEntryUnfocused = new Command(LockEntryValidation);
            AddFriend = false;
        }

        public bool stationIDEntered = false;
        public bool lockIDEntered = false;

        /**
         * A custom behavior has been added to check if there is data
         * in the entries, they are updated if the user is not focused on the entries
         */
        public ICommand FriendEntryUnfocused { get; protected set; }
        public ICommand StationEntryUnfocused { get; protected set; }
        public ICommand LockEntryUnfocused { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }


        private bool _UnlockEnabled;

        public bool UnlockEnabled
        {
            get 
            { 
                return _UnlockEnabled; 
            }
            set 
            { 
                _UnlockEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _LockEnabled;

        public bool LockEnabled
        {
            get
            {
                return _LockEnabled;
            }
            set
            {
                _LockEnabled = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        #region --Command implementations--

        public Command UnlockCMD => new Command(async () =>
        {
           
            LockEnabled = true;
            if (UnlockEnabled == true)
            {
                UnlockEnabled = false;
            }

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine("Unlock CMD works!");
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location");
            }


        });

        public Command LockCMD => new Command(async () =>
        {
            
            UnlockEnabled = true;
            if (LockEnabled == true)
            {
                LockEnabled = false;
            }

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine("Lock CMD works!");
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location");
            }

        });

        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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



    }
}
