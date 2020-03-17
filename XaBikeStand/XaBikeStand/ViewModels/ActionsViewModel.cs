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
    class ActionsViewModel : BaseViewModel, INotifyPropertyChanged
    {


        public ActionsViewModel()
        {
            FriendEntryUnfocused = new Command(FriendEntryValidation);
            StationEntryUnfocused = new Command(StationEntryValidation);
            LockEntryUnfocused = new Command(LockEntryValidation);
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
                OnPropertyChanged();
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
            bool succes = serverClient.Unlock();

            if (succes)
            {
                LockVisible = true;
                UnlockVisible = false;
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
                // error
            }
            Console.WriteLine(  "this worked " + succes);
            if (succes)
            {
                LockVisible = false;
                UnlockVisible = true;
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
