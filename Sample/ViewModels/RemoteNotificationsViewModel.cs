using CommunityToolkit.Mvvm.ComponentModel;
using ActitoSdk.Inbox;
using ActitoSdk.Inbox.Core.Events;
using ActitoSdk.Push;
using ActitoSdk.Push.Core.Events;

namespace Sample.ViewModels;

public partial class RemoteNotificationsViewModel : ObservableObject
{
    [ObservableProperty] private bool _hasNotificationsEnabled;
    [ObservableProperty] private int _badge;

    public RemoteNotificationsViewModel()
    {
        HasNotificationsEnabled = ActitoPush.HasRemoteNotificationsEnabled && ActitoPush.AllowedUI;
        Badge = ActitoInbox.Badge;

        ActitoPush.NotificationSettingsChanged += OnNotificationsSettingsChanged;
        ActitoInbox.BadgeUpdated += OnBadgeChanged;
    }

    #region Setup Listeners

    private void OnNotificationsSettingsChanged(object? sender, ActitoNotificationSettingsChangedEventArgs e)
    {
        HasNotificationsEnabled = ActitoPush.HasRemoteNotificationsEnabled && ActitoPush.AllowedUI;
    }

    private void OnBadgeChanged(object? sender, ActitoBadgeUpdatedEventArgs e)
    {
        Badge = e.Badge;
    }

    #endregion

    #region Remote Notifications

    public async void EnableRemoteNotifications()
    {
        try
        {
            await ActitoPush.EnableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = false;
            Console.WriteLine($"Failed to enable remote notifications: {e.Message}");
        }
    }

    public async void DisableRemoteNotifications()
    {
        try
        {
            await ActitoPush.DisableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = true;
            Console.WriteLine($"Failed to disable remote notifications: {e.Message}");
        }
    }

    #endregion
}
