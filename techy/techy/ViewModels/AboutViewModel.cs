using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace techy.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Techy";
            BacklogCommand = new Command(OnBacklog);
        }

        public ICommand BacklogCommand { get; }


        private async void OnBacklog()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("//ItemsPage");
        }
    }
}