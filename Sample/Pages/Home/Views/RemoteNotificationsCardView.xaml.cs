using ActitoSdk.Push;
using Sample.Pages.Inbox;
using Sample.Pages.Tags;
using Sample.ViewModels;

namespace Sample.Pages.Home.Views;

public partial class RemoteNotificationsCardView : ContentView
{
    public RemoteNotificationsCardView()
    {
        InitializeComponent();
    }

    private void ShowRemoteNotificationsStatusInfo(object sender, EventArgs args)
    {
        var allowedUI = ActitoPush.AllowedUI;
        var enabled = ActitoPush.HasRemoteNotificationsEnabled;
        var transport = ActitoPush.Transport;
        var token = ActitoPush.Subscription;

        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.DisplayAlert(
                title: "Notifications Status",
                message: $"AllowedUI: {allowedUI}" +
                         $"\nEnabled: {enabled}" +
                         $"\nTransport: {transport?.ToString() ?? "NULL"}" +
                         $"\nToken: {token?.Token ?? "NULL"}",
                cancel: "OK"
            );
        });
    }

    private void NavigateToInbox(object sender, EventArgs args)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(InboxPage)));
    }

    private void NavigateToTags(object sender, EventArgs args)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(TagsPage)));
    }

    private void OnNotificationsSwitchToggled(object sender, ToggledEventArgs e)
    {
        var viewModel = (RemoteNotificationsViewModel)BindingContext;

        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (e.Value)
            {
                if (await EnsureNotificationsPermissions())
                {
                    viewModel.EnableRemoteNotifications();
                    return;
                }

                NotificationsSwitch.IsCodeToggled = false;
                return;
            }

            viewModel.DisableRemoteNotifications();
        });
    }

    private async Task<bool> EnsureNotificationsPermissions()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
        if (status == PermissionStatus.Granted) return true;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS) return false;

        if (Permissions.ShouldShowRationale<Permissions.PostNotifications>())
        {
            await Shell.Current.DisplayAlert(
                title: "Notifications Rational",
                message: "Remote notifications rational message.",
                cancel: "OK"
            );
        }

        status = await Permissions.RequestAsync<Permissions.PostNotifications>();

        return status == PermissionStatus.Granted;
    }
}
