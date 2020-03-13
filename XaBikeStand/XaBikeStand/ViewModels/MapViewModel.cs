using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel
    {

        #region --Command implementations--

        public ICommand UnlockCMD => new Command<Button>(async (Button UnlockBike) =>
        {
            Console.WriteLine("Unlock CMD works!");
        });

        public ICommand LockCMD => new Command<Button>(async (Button LockBike) =>
        {
            Console.WriteLine("Lock CMD works!");
        });

        #endregion
    }
}
