using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XaBikeStand.Models;
using XaBikeStand.ViewModels;
using System.Reflection;

namespace XaBikeStand.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetail : MasterDetailPage
    {
		public MasterDetail ()
		{
			InitializeComponent();
            //profileImage.Source = ImageSource.FromFile("spider.jpg");

            navigationList.ItemsSource = GetMenuList();

            IsPresented = false;
        }

        public List<MasterMenuItems> GetMenuList()
        {
            var list = new List<MasterMenuItems>();

            list.Add(new MasterMenuItems() { 
                Text = "Cykelsiden",
                Detail = "Hent, lås eller del din cykel med en ven",
                TargetViewModel = typeof(ActionsViewModel)
            });

            list.Add(new MasterMenuItems()
            {
                Text = "Kort",
                Detail = "Se et kort over mulige standere",
                TargetViewModel = typeof(MapViewModel)
            }) ;

            list.Add(new MasterMenuItems()
            {
                Text = "Om os",
                Detail = "",
                TargetViewModel = typeof(AboutUsViewModel)
            });

            list.Add(new MasterMenuItems
            {
                Text = "Info",
                Detail = "Information",
                TargetViewModel = typeof(InfoViewModel)
            });

            list.Add(new MasterMenuItems() 
            { 
                Text = "Konto",
                Detail = "Konto information",
                TargetViewModel = typeof(AccountViewModel)
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