﻿using System.Collections.Generic;
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
}

        public void ClearSelection()
        {
            navigationList.SelectedItem = null;
        }
    }
}