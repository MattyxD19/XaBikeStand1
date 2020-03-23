﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XaBikeStand.ViewModels;
using XaBikeStand.Views;
using XaBikeStand.Services;
using XaBikeStand.Models;

namespace XaBikeStand.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ISettingsService _settingsService;

        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }

        public NavigationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public Task InitializeAsync()
        {
            //return NavigateToAsync<UserNameViewModel>();
            return NavigateToAsync<LoginViewModel>();//The startpage

            //if (string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            //return NavigateToAsync<MenuViewModel>();
            //else
            //  return NavigateToAsync<MenuViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// Used by the Master Detail Menu
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            //Use Master detail navigation instead
            var mpage = Application.Current.MainPage as MasterDetailPage;
            mpage.Detail = new NavigationPage(page);

            if (viewModelType == typeof(LoginViewModel))
            {
                SingletonSharedData sharedData = SingletonSharedData.GetInstance();

                page.BindingContext = new LoginViewModel();

            }
            else
            {
                await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }
            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
