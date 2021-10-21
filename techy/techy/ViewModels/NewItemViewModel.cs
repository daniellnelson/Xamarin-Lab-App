using Newtonsoft.Json;
using RestSharp;
using SQLite;
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
using Xamarin.Forms;

namespace techy.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class NewItemViewModel : BaseViewModel
    {
        private int itemId;

        private string name;
        private string description;
        private int rating;
        private double time;
        private string completionstatus;
        private string startDate;
        private string endDate;
        private string system;

        private List<Game> games = new List<Game>();
        public List<Game> Games
        {
            get { return games; }
            set { SetProperty(ref games, value); }
        }

        
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                //LoadItemId(value);
            }
        }
        private bool itemListVisible = false;
        public bool ItemListVisible
        {
            get { return itemListVisible; }
            set
            {
                SetProperty(ref itemListVisible, value);
                NoItemsLabelVisible = !ItemListVisible;
            }
        }

        private bool noItemsLabelVisible = false;
        public bool NoItemsLabelVisible
        {
            get { return noItemsLabelVisible; }
            set { SetProperty(ref noItemsLabelVisible, value); }
        }
        public async void getGames()
        {
            var items = await Game.GetItemsAsync().ConfigureAwait(false);
            foreach (var item in items)
            {
                if(item.id == ItemId)
                {
                    Name = item.name;
                }
            }
        }


        public NewItemViewModel()
        {
          //  getGames();


            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }


            private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);
               // && !String.IsNullOrWhiteSpace(startDate);
        }

        public string Name { get; set; }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }
        public double Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
        public string CompletionStatus
        {
            get => completionstatus;
            set => SetProperty(ref completionstatus, value);
        }
        public string System
        {
            get => system;
            set => SetProperty(ref system, value);
        }
        public string StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }
        public string EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        public Command SaveCommand { get; }
        public Command SearchCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

       

        private async void OnSave()
        {

            Item newItem = new Item
            {
                Name = Name,
                Description = Description,
                Rating = Rating,
                Time = Time,
                CompletionStatus = CompletionStatus,
                StartDate = StartDate,
                EndDate = EndDate,
                System = System
            };

            await Item.SaveItemAsync(newItem).ConfigureAwait(false);

            // This will pop the current page off the navigation stack
            try
            {

                Device.BeginInvokeOnMainThread(async () => { await Shell.Current.GoToAsync("//ItemsPage"); });
                
            }
            catch (Exception ex)
            {
                DialogService.ShowMessage(ex.Message, "Error");
            }
            
            
        }
    }
}
