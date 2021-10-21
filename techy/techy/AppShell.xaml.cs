using System;
using System.Collections.Generic;
using techy.ViewModels;
using techy.Views;
using Xamarin.Forms;

namespace techy
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddGameListPage), typeof(AddGameListPage));
            Routing.RegisterRoute(nameof(SearchedGamePage), typeof(SearchedGamePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
