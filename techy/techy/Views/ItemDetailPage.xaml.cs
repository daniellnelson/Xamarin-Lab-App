using System.ComponentModel;
using techy.ViewModels;
using Xamarin.Forms;

namespace techy.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

    }
}