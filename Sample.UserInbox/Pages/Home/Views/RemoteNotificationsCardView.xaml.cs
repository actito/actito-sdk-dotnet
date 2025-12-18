using System.Windows.Input;
using ActitoSdk.Push;
using Sample.UserInbox.Pages.Inbox;

namespace Sample.UserInbox.Pages.Home.Views;

public partial class RemoteNotificationsCardView : ContentView
{
    public static readonly BindableProperty TokenProperty =
        BindableProperty.Create(nameof(Token), typeof(string), typeof(RemoteNotificationsCardView));

    public static readonly BindableProperty HasNotificationsEnabledProperty =
        BindableProperty.Create(nameof(HasNotificationsEnabled), typeof(bool), typeof(RemoteNotificationsCardView),
            false, BindingMode.TwoWay);

    public static readonly BindableProperty BadgeProperty =
        BindableProperty.Create(nameof(Badge), typeof(int), typeof(RemoteNotificationsCardView), 0);

    public static readonly BindableProperty EnableRemoteNotificationsCommandProperty =
        BindableProperty.Create(nameof(EnableRemoteNotificationsCommand), typeof(ICommand),
            typeof(RemoteNotificationsCardView));

    public static readonly BindableProperty DisableRemoteNotificationsCommandProperty =
        BindableProperty.Create(nameof(DisableRemoteNotificationsCommand), typeof(ICommand),
            typeof(RemoteNotificationsCardView));

    public RemoteNotificationsCardView()
    {
        InitializeComponent();
    }

    public string? Token
    {
        get => (string?)GetValue(TokenProperty);
        set => SetValue(TokenProperty, value);
    }

    public bool HasNotificationsEnabled
    {
        get => (bool)GetValue(HasNotificationsEnabledProperty);
        set => SetValue(HasNotificationsEnabledProperty, value);
    }

    public int Badge
    {
        get => (int)GetValue(BadgeProperty);
        set => SetValue(BadgeProperty, value);
    }

    public ICommand EnableRemoteNotificationsCommand
    {
        get => (ICommand)GetValue(EnableRemoteNotificationsCommandProperty);
        set => SetValue(EnableRemoteNotificationsCommandProperty, value);
    }

    public ICommand DisableRemoteNotificationsCommand
    {
        get => (ICommand)GetValue(DisableRemoteNotificationsCommandProperty);
        set => SetValue(DisableRemoteNotificationsCommandProperty, value);
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
                "Notifications Status",
                $"AllowedUI: {allowedUI}" +
                $"\nEnabled: {enabled}" +
                $"\nTransport: {transport?.ToString() ?? "NULL"}" +
                $"\nToken: {token?.Token ?? "NULL"}",
                "OK"
            );
        });
    }

    private void NavigateToInbox(object sender, EventArgs args)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
            await Shell.Current.GoToAsync($"{nameof(InboxPage)}?token={Token}")
        );
    }

    private void OnNotificationsSwitchToggled(object sender, ToggledEventArgs e)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (e.Value)
            {
                if (await EnsureNotificationsPermissions())
                {
                    EnableRemoteNotificationsCommand.Execute(null);
                    return;
                }

                NotificationsSwitch.IsCodeToggled = false;
                return;
            }

            DisableRemoteNotificationsCommand.Execute(null);
        });
    }

    private async Task<bool> EnsureNotificationsPermissions()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
        if (status == PermissionStatus.Granted) return true;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS) return false;

        if (Permissions.ShouldShowRationale<Permissions.PostNotifications>())
            await Shell.Current.DisplayAlert(
                "Notifications Rational",
                "Remote notifications rational message.",
                "OK"
            );

        status = await Permissions.RequestAsync<Permissions.PostNotifications>();

        return status == PermissionStatus.Granted;
    }
}
