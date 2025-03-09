using Syncfusion.Blazor.Notifications;

namespace Microsis.Web.Reserved.Shared
{
    public static class ToastSettings
    {
        public static SfToast ToastInstance { get; set; }

        public static async Task ShowSuccess(string message)
        {
            if (ToastInstance != null)
            {
                ToastModel toast = new ToastModel
                {
                    Title = "Successo",
                    Content = message,
                    CssClass = "e-toast-success",
                    Icon = "e-success toast-icons",
                    ShowCloseButton = true,
                    Timeout = 3000
                };
                await ToastInstance.ShowAsync(toast);
            }
        }

        public static async Task ShowError(string message)
        {
            if (ToastInstance != null)
            {
                ToastModel toast = new ToastModel
                {
                    Title = "Errore",
                    Content = message,
                    CssClass = "e-toast-danger",
                    Icon = "e-error toast-icons",
                    ShowCloseButton = true,
                    Timeout = 5000
                };
                await ToastInstance.ShowAsync(toast);
            }
        }

        public static async Task ShowWarning(string message)
        {
            if (ToastInstance != null)
            {
                ToastModel toast = new ToastModel
                {
                    Title = "Attenzione",
                    Content = message,
                    CssClass = "e-toast-warning",
                    Icon = "e-warning toast-icons",
                    ShowCloseButton = true,
                    Timeout = 4000
                };
                await ToastInstance.ShowAsync(toast);
            }
        }

        public static async Task ShowInfo(string message)
        {
            if (ToastInstance != null)
            {
                ToastModel toast = new ToastModel
                {
                    Title = "Informazione",
                    Content = message,
                    CssClass = "e-toast-info",
                    Icon = "e-info toast-icons",
                    ShowCloseButton = true,
                    Timeout = 3000
                };
                await ToastInstance.ShowAsync(toast);
            }
        }
    }
}
