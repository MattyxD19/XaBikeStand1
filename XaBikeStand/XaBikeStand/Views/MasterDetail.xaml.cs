using System.Collections.Generic;
using XaBikeStand.Models;
using XaBikeStand.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XaBikeStand.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetail : MasterDetailPage
    {
        public MasterDetail()
        {
            InitializeComponent();
            //profileImage.Source = ImageSource.FromFile("spider.jpg");

            navigationList.ItemsSource = GetMenuList();

            IsPresented = false;
        }

        public List<MasterMenuItems> GetMenuList()
        {
            var list = new List<MasterMenuItems>();

            list.Add(new MasterMenuItems()
            {
                Text = "Bike page",
                ImagePath = "BikeLogo.png",
                TargetViewModel = typeof(ActionsViewModel)
            });

            list.Add(new MasterMenuItems()
            {
                Text = "Map",
                ImagePath = "MapLogo.png",
                TargetViewModel = typeof(MapViewModel)
            });

            //list.Add(new MasterMenuItems()
            //{
            //    Text = "Om os",
            //    ImagePath = "MapLogo.png",
            //    TargetViewModel = typeof(AboutUsViewModel)
            //});

            //list.Add(new MasterMenuItems
            //{
            //    Text = "Info",
            //    ImagePath = "MapLogo.png",
            //    TargetViewModel = typeof(InfoViewModel)
            //});



            list.Add(new MasterMenuItems()
            {
                Text = "Account",
                ImagePath = "AccountLogo.png",
                TargetViewModel = typeof(AccountViewModel)
            });

            list.Add(new MasterMenuItems()
            {
                Text = "Log out",
                ImagePath = "LogOutLogo.png",
                TargetViewModel = typeof(LoginViewModel)
            });

            return list;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedMenuItem = (MasterMenuItems)e.SelectedItem;

            var viewModel = (ViewModels.MasterDetailViewModel)this.BindingContext;
            viewModel.ChangeVMCMD.Execute(selectedMenuItem);

            IsPresented = false;
        }
    }
}