using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ActitoSdk.Inbox;
using ActitoSdk.Inbox.Core.Events;
using ActitoSdk.Inbox.Core.Models;
using ActitoSdk.Push.UI;

#if IOS
using UIKit;
#endif

namespace Sample.ViewModels;

public partial class InboxViewModel : ObservableObject
{
    [ObservableProperty] private IList<ActitoInboxItem> _items;

    public InboxViewModel()
    {
        Items = ActitoInbox.Items;
    }

    public void SetupListeners()
    {
        ActitoInbox.InboxUpdated += OnInboxUpdated;
    }

    public void CleanListeners()
    {
        ActitoInbox.InboxUpdated -= OnInboxUpdated;
    }

#if ANDROID
    internal void Open(ActitoInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
        try
        {
            var notification = await ActitoInbox.OpenAsync(item);
            var activity = Platform.CurrentActivity;
            ActitoPushUI.PresentNotification(notification, activity!);

            Console.WriteLine("Opened and presented inbox item successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to open inbox item: {e.Message}");
        }
        });
    }

#elif IOS
    internal void Open(ActitoInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                var notification = await ActitoInbox.OpenAsync(item);
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to open inbox item: {e.Message}");
            }
        });
    }
#endif

    internal void MarkAsRead(ActitoInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoInbox.MarkAsReadAsync(item);
                Console.WriteLine("Marked as read inbox item successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to mark as read inbox item: {e.Message}");
            }
        });
    }

    internal void Remove(ActitoInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoInbox.RemoveAsync(item);
                Console.WriteLine("Removed inbox item successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to remove inbox item: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void MarkAllAsRead()
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoInbox.MarkAllAsReadAsync();
                Console.WriteLine("Marked as read all inbox items successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to mark as read all inbox items: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void Clear()
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoInbox.ClearAsync();
                Console.WriteLine("Cleared inbox successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to clear inbox: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void Refresh()
    {
        Task.Run(async () =>
        {
            try
            {
                await ActitoInbox.RefreshAsync();
                Console.WriteLine("Cleared inbox successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to clear inbox: {e.Message}");
            }
        });
    }

    private void OnInboxUpdated(object? sender, ActitoInboxUpdatedEventArgs e)
    {
        Items = e.Items;
    }
}
