using System.ComponentModel;
using techy.ViewModels;
using Xamarin.Forms;

namespace techy.Views
{
    public partial class EditPage : ContentPage
    {
        public EditPage()
        {
            InitializeComponent();
            BindingContext = new EditViewModel();
        }

    }
}