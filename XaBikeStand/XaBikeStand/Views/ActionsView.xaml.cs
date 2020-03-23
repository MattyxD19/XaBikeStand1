using System;
using XaBikeStand.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XaBikeStand.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionsView : ContentPage
    {
        public ActionsView()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            ((ActionsViewModel)this.BindingContext).OnAppearing();
        }
    }
}