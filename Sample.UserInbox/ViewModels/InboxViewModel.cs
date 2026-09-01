using ActitoSdk.Push;
using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.UI;
using ActitoSdk.UserInbox;
using ActitoSdk.UserInbox.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.UserInbox.Network;
#if IOS
using UIKit;
#endif

namespace Sample.UserInbox.ViewModels;

public partial class InboxViewModel : ObservableObject
{
    internal string? AccessToken;
    private readonly UserInboxService _userInboxService;

    [ObservableProperty] private IList<ActitoUserInboxItem> _items = [];

    public InboxViewModel(UserInboxService userInboxService)
    {
        _userInboxService = userInboxService;
    }

    public void SetupListeners()
    {
        ActitoPush.NotificationReceived += OnNotificationReceived;
        ActitoPush.NotificationOpened += OnNotificationOpened;
    }

    public void CleanListeners()
    {
        ActitoPush.NotificationReceived -= OnNotificationReceived;
        ActitoPush.NotificationOpened -= OnNotificationOpened;
    }

    private void OnNotificationReceived(object? sender, ActitoNotificationReceivedEventArgs e)
    {
        Refresh();
    }

    private void OnNotificationOpened(object? sender, ActitoNotificationOpenedEventArgs e)
    {
        Refresh();
    }

#if ANDROID
    internal void Open(ActitoUserInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                var notification = await ActitoUserInbox.OpenAsync(item);
                var activity = Platform.CurrentActivity;
                ActitoPushUI.PresentNotification(notification, activity!);

                Console.WriteLine("Opened and presented inbox item successfully.");

                if (!item.Opened) Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to open inbox item: {e.Message}");
            }
        });
    }

#elif IOS
    internal void Open(ActitoUserInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                var notification = await ActitoUserInbox.OpenAsync(item);
                var rootViewController = UIApplication.SharedApplication.ConnectedScenes
                    .OfType<UIWindowScene>()
                    .SelectMany(scene => scene.Windows)
                    .FirstOrDefault(window => window.IsKeyWindow)?
                    .RootViewController;

                if (rootViewController is null)
                {
                    Console.WriteLine("Cannot present a notification with a null root view controller.");
                    return;
                }

                if (notification.RequiresViewController())
                {
                    var navigationController = new UINavigationController();
                    if (navigationController.View is not null)
                        navigationController.View.BackgroundColor = UIColor.SystemBackground;

                    rootViewController.PresentViewController(
                        navigationController,
                        true,
                        () => ActitoPushUI.PresentNotification(notification, navigationController)
                    );
                }
                else
                {
                    ActitoPushUI.PresentNotification(notification, rootViewController);
                }

                Console.WriteLine("Opened and presented inbox item successfully.");

                if (!item.Opened) Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to open inbox item: {e.Message}");
            }
        });
    }
#endif

    internal void MarkAsRead(ActitoUserInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoUserInbox.MarkAsReadAsync(item);
                Console.WriteLine("Marked as read inbox item successfully.");

                if (!item.Opened) Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to mark as read inbox item: {e.Message}");
            }
        });
    }

    internal void Remove(ActitoUserInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoUserInbox.RemoveAsync(item);
                Console.WriteLine("Removed inbox item successfully.");

                Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to remove inbox item: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void Refresh()
    {
        var token = AccessToken;
        if (token == null)
        {
            Console.WriteLine("Could not refresh inbox. Access token is required");
            return;
        }

        Task.Run(async () =>
        {
            try
            {
                var requestResponse = await _userInboxService.GetInboxResponse(token);
                var inboxResponse = await ActitoUserInbox.ParseResponseAsync(requestResponse);
                Items = inboxResponse.Items;
            }
            catch (Exception e)
            {
                Items = [];
                Console.WriteLine($"Failed to refresh inbox: {e.Message}");
            }
        });
    }
}
