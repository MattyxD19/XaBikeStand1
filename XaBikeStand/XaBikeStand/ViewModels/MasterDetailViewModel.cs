using System.Collections.Generic;
using XaBikeStand.Models;
using XaBikeStand.Views;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class MasterDetailViewModel : BaseViewModel
    {
        #region --Binding properties--
        private bool isPresented;

        public bool IsPresented
        {
            get { return isPresented; }
            set { isPresented = value; propertyIsChanged(); }
        }

        private MasterMenuItems selectedItem;

        public MasterMenuItems SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                if (selectedItem == null)
                    return;
                ChangeView(selectedItem);
                SelectedItem = null;
            }
        }

        private List<MasterMenuItems> menuItems;

        public List<MasterMenuItems> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }
        #endregion


        public MasterDetailViewModel()
        {
            MenuItems = new List<MasterMenuItems>();

            MenuItems.Add(new MasterMenuItems()
            {
                Text = "Cykelsiden",
                ImagePath = "BikeLogo.png",
                TargetViewModel = typeof(ActionsViewModel)
            });

            MenuItems.Add(new MasterMenuItems()
            {
                Text = "Kort",
                ImagePath = "MapLogo.png",
                TargetViewModel = typeof(MapViewModel)
            });

            MenuItems.Add(new MasterMenuItems()
            {
                Text = "Konto",
                ImagePath = "AccountLogo.png",
                TargetViewModel = typeof(AccountViewModel)
            });

            MenuItems.Add(new MasterMenuItems()
            {
                Text = "Log ud",
                ImagePath = "LogOutLogo.png",
                TargetViewModel = typeof(LoginViewModel)
            });
        }

        /// <summary>
        /// Changes the view to the desired view from the listview of menuitems
        /// </summary>
        /// <param name="mmi"></param>
        private async void ChangeView(MasterMenuItems mmi)
        {
            IsPresented = false;
            ((MasterDetail)(MasterDetailPage)Application.Current.MainPage).ClearSelection();
            await NavigationService.NavigateToAsync(mmi.TargetViewModel);
        }
    }
}
