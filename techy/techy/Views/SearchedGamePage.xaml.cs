using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using techy.Models;
using techy.Utils;
using techy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace techy.Views
{
    public partial class SearchedGamePage : ContentPage
    {

        SearchedGameViewModel _viewModel;

        public SearchedGamePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SearchedGameViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}