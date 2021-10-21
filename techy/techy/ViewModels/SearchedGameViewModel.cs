using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using techy.Models;
using techy.Utils;
using techy.Views;
using Xamarin.Forms;

namespace techy.ViewModels
{
    public class SearchedGameViewModel : BaseViewModel
    {
        private Game _selectedItem;
        private int index = 0;

        public ObservableCollection<Game> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Game> ItemTapped { get; }

        public Command SearchCommand { get; }

        public SearchedGameViewModel()
        {
            Title = "Searcing Games";
            Items = new ObservableCollection<Game>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Game>(OnItemSelected);
            SearchCommand = new Command(OnSearchGame);
            AddItemCommand = new Command(OnAddItem);
        }
        private string name;
        private bool loadedFindings = false;
        public bool LoadedFindings
        {
            get { return loadedFindings; }
            set { SetProperty(ref loadedFindings, value); }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        async Task ExecuteLoadItemsCommand()
        {
           // IsBusy = true;

            try
            {
                Items.Clear();
                var items = await Game.GetItemsAsync().ConfigureAwait(false);
                if(items.Count == 0)
                {
                    DialogService.ShowMessage("No games found. Please search again.", "No Result");
                }
                foreach (var item in items)
                {
                    Items.Add(item);
                   // DialogService.ShowMessage(item.name, "Found a game, adding to list");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
               // IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Game SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }


        private async void OnSearchGame(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddGameListPage));
            }
            catch (Exception ex)
            {
                DialogService.ShowMessage(ex.Message, "Error");
            }
        }


        async void OnItemSelected(Game item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(NewItemViewModel.Name)}={item.name}");
        }


    }
}