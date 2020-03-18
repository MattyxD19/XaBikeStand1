using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XaBikeStand.ViewModels
{
    class LoginViewModel : BaseViewModel
    {


        public ICommand ChangeVMCMD => new Command(async () => {

            await NavigationService.NavigateToAsync(typeof(ActionsViewModel));

        });

    }
}
