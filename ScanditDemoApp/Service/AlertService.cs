using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace ScanditDemoApp.Service
{
    public interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel = "OK");
        void ShowAlert(string title, string message, string cancel = "OK");
        Task ShowToastAsync(string message, ToastDuration toastDuration = ToastDuration.Short, double textSize = 14.0);
        void ShowToast(string message, ToastDuration toastDuration = ToastDuration.Short, double textSize = 14.0);
        Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No");
        void ShowConfirmation(string title, string message, Action<bool> callback,
                              string accept = "Yes", string cancel = "No");
    }

    internal class AlertService : IAlertService
    {
        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return AppShell.Current.DisplayAlert(title, message, cancel);
        }

        public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return AppShell.Current.DisplayAlert(title, message, accept, cancel);
        }

        public void ShowAlert(string title, string message, string cancel = "OK")
        {
            AppShell.Current.Dispatcher.Dispatch(async () =>
                await ShowAlertAsync(title, message, cancel)
            );
        }

        public void ShowToast(string message, ToastDuration toastDuration = ToastDuration.Short, double textSize = 14.0)
        {
            AppShell.Current.Dispatcher.Dispatch(async () =>
                await ShowToastAsync(message, toastDuration, textSize)
            );
        }

        public async Task ShowToastAsync(string message, ToastDuration toastDuration = ToastDuration.Short, double textSize = 14.0)
        {
            await Toast.Make(message, toastDuration, textSize).Show();
        }


        public void ShowConfirmation(string title, string message, Action<bool> callback,
                                     string accept = "Yes", string cancel = "No")
        {
            AppShell.Current.Dispatcher.Dispatch(async () =>
            {
                bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
                callback(answer);
            });
        }

        public async Task<string> ShowActionSheetAsync(string title, string cancel, params string[] actions)
        {
            return await AppShell.Current.DisplayActionSheet(title, cancel, null, actions);
        }

        public void ShowActionSheet(string title, string cancel, Action<string> callback, params string[] actions)
        {
            AppShell.Current.Dispatcher.Dispatch(async () =>
            {
                var result = await ShowActionSheetAsync(title, cancel, actions);
                callback(result);
            });
        }
    }
}
