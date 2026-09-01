using ActitoSdk.iOS.Internal;
using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.Core.Internal;
using ActitoSdk.Push.Core.Models;
using ActitoSdk.Push.iOS.Internal;
using UserNotifications;

namespace ActitoSdk.Push.iOS;

public class ActitoPushPlatformIos : IActitoPushPlatform
{
    private InternalActitoPushDelegate? _delegate;
    private Binding.ActitoPushNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoPushDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoNotificationReceivedEventArgs>? NotificationReceived;
    public event EventHandler<ActitoSystemNotificationReceivedEventArgs>? SystemNotificationReceived;
    public event EventHandler<ActitoUnknownNotificationReceivedEventArgs>? UnknownNotificationReceived;
    public event EventHandler<ActitoNotificationOpenedEventArgs>? NotificationOpened;
    public event EventHandler<ActitoUnknownNotificationOpenedEventArgs>? UnknownNotificationOpened;
    public event EventHandler<ActitoNotificationActionOpenedEventArgs>? NotificationActionOpened;
    public event EventHandler<ActitoUnknownNotificationActionOpenedEventArgs>? UnknownNotificationActionOpened;
    public event EventHandler<ActitoNotificationSettingsChangedEventArgs>? NotificationSettingsChanged;
    public event EventHandler<ActitoPushSubscriptionChangedEventArgs>? SubscriptionChanged;
    public event EventHandler<ActitoShouldOpenNotificationSettingsEventArgs>? ShouldOpenNotificationSettings;

    public bool HasRemoteNotificationsEnabled => _native.HasRemoteNotificationsEnabled;

    public ActitoTransport? Transport
    {
        get
        {
            var transport = _native.Transport;
            return NativeConverter.FromNativeTransport(transport);
        }
    }

    public ActitoPushSubscription? Subscription
    {
        get
        {
            var subscription = _native.Subscription;
            return subscription == null ? null : NativeConverter.FromNativeSubscription(subscription);
        }
    }

    public bool AllowedUI => _native.AllowedUI;

    public Task EnableRemoteNotificationsAsync()
    {
        TaskCompletionSource completion = new();

        _native.EnableRemoteNotifications(
            _ => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task DisableRemoteNotificationsAsync()
    {
        TaskCompletionSource completion = new();

        _native.DisableRemoteNotifications(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public void SetAuthorizationOptions(IList<ActitoAuthorizationOptions> authorizationOptions)
    {
        if (authorizationOptions.Count == 0)
        {
            _native.AuthorizationOptions = UNAuthorizationOptions.None;
            return;
        }

        var options = UNAuthorizationOptions.None;

        foreach (var option in authorizationOptions)
        {
            switch (option)
            {
                case ActitoAuthorizationOptions.Alert:
                    options |= UNAuthorizationOptions.Alert;
                    break;
                case ActitoAuthorizationOptions.Badge:
                    options |= UNAuthorizationOptions.Badge;
                    break;
                case ActitoAuthorizationOptions.Sound:
                    options |= UNAuthorizationOptions.Sound;
                    break;
                case ActitoAuthorizationOptions.CarPlay:
                    options |= UNAuthorizationOptions.CarPlay;
                    break;
                case ActitoAuthorizationOptions.ProvidesAppNotificationSettings:
                    options |= UNAuthorizationOptions.ProvidesAppNotificationSettings;
                    break;
                case ActitoAuthorizationOptions.Provisional:
                    options |= UNAuthorizationOptions.Provisional;
                    break;
                case ActitoAuthorizationOptions.CriticalAlert:
                    options |= UNAuthorizationOptions.CriticalAlert;
                    break;
                case ActitoAuthorizationOptions.Announcement:
                    // iOS 15 and later UNAuthorizationOptions.Announcement is always included (https://developer.apple.com/documentation/usernotifications/unauthorizationoptions/announcement)
                    if (!OperatingSystem.IsIOSVersionAtLeast(15))
                        options |= UNAuthorizationOptions.Announcement;
                    break;
            }
        }

        _native.AuthorizationOptions = options;
    }

    public void SetCategoryOptions(IList<ActitoCategoryOptions> categoryOptions)
    {
        if (categoryOptions.Count == 0)
        {
            _native.CategoryOptions = UNNotificationCategoryOptions.None;
            return;
        }

        var options = UNNotificationCategoryOptions.None;

        foreach (var option in categoryOptions)
        {
            switch (option)
            {
                case ActitoCategoryOptions.CustomDismissAction:
                    options |= UNNotificationCategoryOptions.CustomDismissAction;
                    break;
                case ActitoCategoryOptions.AllowInCarPlay:
                    options |= UNNotificationCategoryOptions.AllowInCarPlay;
                    break;
                case ActitoCategoryOptions.HiddenPreviewsShowTitle:
                    options |= UNNotificationCategoryOptions.HiddenPreviewsShowTitle;
                    break;
                case ActitoCategoryOptions.HiddenPreviewsShowSubtitle:
                    options |= UNNotificationCategoryOptions.HiddenPreviewsShowSubtitle;
                    break;
                case ActitoCategoryOptions.AllowAnnouncement:
                    options |= UNNotificationCategoryOptions.AllowAnnouncement;
                    break;
            }
        }

        _native.CategoryOptions = options;
    }

    public void SetPresentationOptions(IList<ActitoPresentationOptions> presentationOptions)
    {
        if (presentationOptions.Count == 0)
        {
            _native.PresentationOptions = UNNotificationPresentationOptions.None;
            return;
        }

        var options = UNNotificationPresentationOptions.None;

        foreach (var option in presentationOptions)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(14))
            {
                if (option is ActitoPresentationOptions.Banner or ActitoPresentationOptions.Alert)
                    options |= UNNotificationPresentationOptions.Banner;

                if (option is ActitoPresentationOptions.List)
                    options |= UNNotificationPresentationOptions.List;
            }
            else
            {
                if (option is ActitoPresentationOptions.Alert)
                    options |= UNNotificationPresentationOptions.Alert;
            }

            if (option is ActitoPresentationOptions.Badge)
                options |= UNNotificationPresentationOptions.Badge;

            if (option is ActitoPresentationOptions.Sound)
                options |= UNNotificationPresentationOptions.Sound;
        }

        _native.PresentationOptions = options;
    }

    public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
    {
        _native.RegisteredForRemoteNotifications(application, deviceToken);
    }

    public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
    {
        _native.FailedToRegisterForRemoteNotifications(application, error);
    }

    public void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    )
    {
        _native.DidReceiveRemoteNotification(application, userInfo, completionHandler);
    }

    public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
    {
        _native.WillPresentNotification(center, notification, completionHandler);
    }

    public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response,
        Action completionHandler)
    {
        _native.DidReceiveNotificationResponse(center, response, completionHandler);
    }

    public void OpenSettings(UNUserNotificationCenter center, UNNotification? notification)
    {
        _native.OpenSettings(center, notification);
    }


    private sealed class InternalActitoPushDelegate : Binding.ActitoPushNativeBindingDelegate
    {
        private readonly ActitoPushPlatformIos _platform;

        internal InternalActitoPushDelegate(ActitoPushPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidChangeSubscription(
            Binding.ActitoPushNativeBinding actitoPush,
            Binding.ActitoPushSubscription? subscription
        )
        {
            _platform.SubscriptionChanged?.Invoke(
                _platform,
                new ActitoPushSubscriptionChangedEventArgs(
                    subscription: subscription == null ? null : NativeConverter.FromNativeSubscription(subscription)
                )
            );
        }

        public override void DidChangeNotificationSettings(
            Binding.ActitoPushNativeBinding actitoPush,
            bool allowedUI
        )
        {
            _platform.NotificationSettingsChanged?.Invoke(
                _platform,
                new ActitoNotificationSettingsChangedEventArgs(
                    allowedUI: allowedUI
                )
            );
        }

        public override void DidReceiveNotification(
            Binding.ActitoPushNativeBinding actitoPush,
            ActitoSdk.iOS.Binding.ActitoNotification notification,
            Binding.ActitoNotificationDeliveryMechanism deliveryMechanism
        )
        {
            _platform.NotificationReceived?.Invoke(
                _platform,
                new ActitoNotificationReceivedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification),
                    deliveryMechanism: NativeConverter.FromNativeDeliveryMechanism(deliveryMechanism)
                )
            );
        }

        public override void DidReceiveSystemNotification(
            Binding.ActitoPushNativeBinding actitoPush,
            Binding.ActitoSystemNotification notification
        )
        {
            _platform.SystemNotificationReceived?.Invoke(
                _platform,
                new ActitoSystemNotificationReceivedEventArgs(
                    notification: NativeConverter.FromNativeSystemNotification(notification)
                )
            );
        }

        public override void DidReceiveUnknownNotification(
            Binding.ActitoPushNativeBinding actitoPush,
            NSDictionary userInfo
        )
        {
            _platform.UnknownNotificationReceived?.Invoke(
                _platform,
                new ActitoUnknownNotificationReceivedEventArgs(
                    notification: ActitoNativeConverter.FromNativeExtraDictionary(userInfo)
                )
            );
        }

        public override void ShouldOpenSettings(
            Binding.ActitoPushNativeBinding actitoPush,
            ActitoSdk.iOS.Binding.ActitoNotification? notification
        )
        {
            _platform.ShouldOpenNotificationSettings?.Invoke(
                _platform,
                new ActitoShouldOpenNotificationSettingsEventArgs(
                    notification: notification == null
                        ? null
                        : ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidOpenNotification(
            Binding.ActitoPushNativeBinding actitoPush,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationOpened?.Invoke(
                _platform,
                new ActitoNotificationOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidOpenUnknownNotification(
            Binding.ActitoPushNativeBinding actitoPush,
            NSDictionary userInfo
        )
        {
            _platform.UnknownNotificationOpened?.Invoke(
                _platform,
                new ActitoUnknownNotificationOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeExtraDictionary(userInfo)
                )
            );
        }

        public override void DidOpenAction(
            Binding.ActitoPushNativeBinding actitoPush,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationActionOpened?.Invoke(
                _platform,
                new ActitoNotificationActionOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification),
                    action: ActitoNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public override void DidOpenUnknownAction(
            Binding.ActitoPushNativeBinding actitoPush,
            string action,
            NSDictionary notification,
            string? responseText
        )
        {
            _platform.UnknownNotificationActionOpened?.Invoke(
                _platform,
                new ActitoUnknownNotificationActionOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeExtraDictionary(notification),
                    action: action,
                    responseText: responseText
                )
            );
        }
    }
}
