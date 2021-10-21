using System;
using techy.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using techy.Models;
using techy.ViewModels;
using System.Collections.Generic;

namespace techy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddGameListPage : ContentPage
    {

        public AddGameListPage()
        {

            InitializeComponent();

            BindingContext = new AddGameListViewModel();
        }


    }
}
