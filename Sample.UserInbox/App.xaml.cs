using System.Text.Json;
using ActitoSdk;
using ActitoSdk.Push;
using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.UI;
#if IOS
using UIKit;
#endif

namespace Sample.UserInbox;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        ActitoPush.NotificationOpened += OnNotificationOpened;
        ActitoPush.NotificationActionOpened += OnNotificationActionOpened;

        StartMonitoringEvents();
    }

    private void OnNotificationOpened(object? sender, ActitoNotificationOpenedEventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        ActitoPushUI.PresentNotification(e.Notification, activity!);

#elif IOS
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

        if (e.Notification.RequiresViewController())
        {
            var navigationController = new UINavigationController();
            if (navigationController.View is not null)
                navigationController.View.BackgroundColor = UIColor.SystemBackground;

            rootViewController.PresentViewController(
                navigationController,
                true,
                () => ActitoPushUI.PresentNotification(e.Notification, navigationController)
            );
        }
        else
        {
            ActitoPushUI.PresentNotification(e.Notification, rootViewController);
        }
#endif
    }

    private void OnNotificationActionOpened(object? sender, ActitoNotificationActionOpenedEventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        ActitoPushUI.PresentAction(e.Notification, e.Action, activity!);

#elif IOS
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

        ActitoPushUI.PresentAction(e.Notification, e.Action, rootViewController);

#endif
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override async void OnAppLinkRequestReceived(Uri uri)
    {
        base.OnAppLinkRequestReceived(uri);

        Console.WriteLine("Deep link received:  " + uri);

        await Dispatcher.DispatchAsync(async () =>
        {
            await Windows[0].Page!.DisplayAlert("Deep link received", uri.ToString(), "OK");
        });
    }

    private void StartMonitoringEvents()
    {
        //
        // Actito events
        //

        Actito.Ready += async (sender, args) => { LogEvent("ACTITO READY EVENT", args.Application); };

        Actito.Unlaunched += async (sender, args) => { LogEvent("ACTITO UNLAUNCHED EVENT"); };

        Actito.DeviceRegistered += async (sender, args) => { LogEvent("ACTITO DEVICE REGISTERED EVENT", args.Device); };

        //
        // Actito Push events
        //

        ActitoPush.NotificationReceived += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH NOTIFICATION RECEIVED EVENT", args.Notification);
        };

        ActitoPush.SystemNotificationReceived += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH SYSTEM NOTIFICATION RECEIVED EVENT", args.Notification);
        };

        ActitoPush.UnknownNotificationReceived += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH UNKNOWN NOTIFICATION RECEIVED EVENT", args.Notification);
        };

        ActitoPush.NotificationOpened += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH NOTIFICATION OPENED EVENT", args.Notification);
        };

        ActitoPush.NotificationActionOpened += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH NOTIFICATION ACTION OPENED EVENT", args.Action);
        };

        ActitoPush.UnknownNotificationOpened += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH UNKNOWN NOTIFICATION OPENED EVENT", args.Notification);
        };

        ActitoPush.UnknownNotificationActionOpened += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH UNKNOWN NOTIFICATION ACTION OPENED EVENT", args.Action);
        };

        ActitoPush.NotificationSettingsChanged += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH NOTIFICATION SETTINGS CHANGED EVENT", args.AllowedUI);
        };

        ActitoPush.SubscriptionChanged += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH NOTIFICATION SUBSCRIPTION CHANGED EVENT", args.Subscription?.Token ?? "null");
        };

#if IOS
        ActitoPush.ShouldOpenNotificationSettings += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH SHOULD OPEN NOTIFICATION SETTINGS EVENT", args.Notification);
        };
#endif

        //
        // Actito Push UI events
        //

        ActitoPushUI.NotificationWillPresent += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI NOTIFICATION WILL PRESENT EVENT", args.Notification);
        };

        ActitoPushUI.NotificationPresented += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI NOTIFICATION PRESENTED EVENT", args.Notification);
        };

        ActitoPushUI.NotificationFinishedPresenting += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI NOTIFICATION FINISHED PRESENTING EVENT", args.Notification);
        };

        ActitoPushUI.NotificationFailedToPresent += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI NOTIFICATION FAILED TO PRESENT EVENT", args.Notification);
        };

        ActitoPushUI.NotificationUrlClicked += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI NOTIFICATION URL CLICKED EVENT", args.Url);
        };

        ActitoPushUI.ActionWillExecute += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI ACTION WILL EXECUTE EVENT", args.Action);
        };

        ActitoPushUI.ActionExecuted += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI ACTION EXECUTED EVENT", args.Action);
        };

        ActitoPushUI.ActionNotExecuted += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI ACTION NOT EXECUTED EVENT", args.Action);
        };

        ActitoPushUI.ActionFailedToExecute += async (sender, args) =>
        {
            LogEvent("ACTITO.PUSH.UI ACTION FAILED TO EXECUTED EVENT");
            LogEvent("ACTION", args.Action);
            LogEvent("ERROR", args.Error);
        };
    }

    private void LogEvent(string message, object? prop = null)
    {
        if (prop != null)
        {
            var json = JsonSerializer.Serialize(prop, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine($"{message}:\n{json}");
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}
