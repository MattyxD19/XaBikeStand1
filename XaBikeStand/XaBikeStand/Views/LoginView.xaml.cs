﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XaBikeStand.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        async void SignUp_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginView());
        }

        async void Login_Clicked(object sender, System.EventArgs e)
        {

            var result = await this.DisplayAlert("Congratulation", "Login Successful", "Yes", "Cancel");






        }
    }
}
