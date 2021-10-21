using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace techy.Utils
{
    public class DialogService
    {
        public static async Task ShowError(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }

        public static async Task ShowError(
            Exception error,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                error.Message,
                buttonText);

            afterHideCallback?.Invoke();
        }

        public static void ShowMessage(
            string message,
            string title)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage.DisplayAlert(title, message, "OK");
            });
        }

        public static void ShowMessage(
            string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(
                    title,
                    message,
                    buttonText);

                afterHideCallback?.Invoke();
            });
        }
    }
}