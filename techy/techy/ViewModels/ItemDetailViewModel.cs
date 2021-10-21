using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using techy.Models;
using techy.Utils;
using Xamarin.Forms;

namespace techy.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {

        private ObservableCollection<Item> _itemList;
        public ObservableCollection<Item> ItemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                OnPropertyChanged("ItemList");
            }
        }
        private int itemId;
        private string name;
        private string description;
        private int rating;
        private double time;
        private string completionstatus;
        private string startDate;
        private string endDate;
        private string system;


        public Command DeleteCommand { get; }
        public Command SaveCommand { get; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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
                LoadItemId(value);
            }
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
        public ItemDetailViewModel()
        {

            DeleteCommand = new Command(OnDelete);
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }
        private async void OnSave()
        {
            Item item = await Item.GetItemAsync(this.ItemId);

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
            await Item.DeleteItemAsync(item).ConfigureAwait(false);

            // This will pop the current page off the navigation stack
            try
            {

                Device.BeginInvokeOnMainThread(async () => { await Shell.Current.GoToAsync(".."); });

            }
            catch (Exception ex)
            {
                DialogService.ShowMessage(ex.Message, "Error");
            }


        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(startDate);
        }

        private async void OnDelete()
        {
            Item item = await Item.GetItemAsync(this.ItemId);
            try 
            {
                var result = await Item.DeleteItemAsync(item).ConfigureAwait(false);
                await Shell.Current.GoToAsync("..");
            }
            catch(Exception ex)
            {
                DialogService.ShowMessage("Error trying to delete item.", "Error");
            }
            

        }

        private async void OnEdit()
        {
            Item item = await Item.GetItemAsync(this.ItemId);
            try
            {

                //await Shell.Current.GoToAsync($"{nameof(EditViewModel)}?{nameof(EditViewModel.ItemId)}={item.ID}"); 

            }
            catch (Exception ex)
            {
                DialogService.ShowMessage("Error trying to edit item.", "Error");
            }


        }

        public async void LoadItemId(int itemId)
        {
            
            try
            {
                var item = await Item.GetItemAsync(itemId);
                Name = item.Name;
                Description = item.Description;
                Rating = item.Rating;
                Time = item.Time;
                CompletionStatus = item.CompletionStatus;
                StartDate = item.StartDate;
                EndDate = item.EndDate;
                System = item.System;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
