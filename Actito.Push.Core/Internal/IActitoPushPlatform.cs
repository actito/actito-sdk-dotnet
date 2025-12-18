using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.Core.Internal;

public interface IActitoPushPlatform
{
    void Initialize();

    event EventHandler<ActitoNotificationReceivedEventArgs> NotificationReceived;

    event EventHandler<ActitoSystemNotificationReceivedEventArgs> SystemNotificationReceived;

    event EventHandler<ActitoUnknownNotificationReceivedEventArgs> UnknownNotificationReceived;

    event EventHandler<ActitoNotificationOpenedEventArgs> NotificationOpened;

    event EventHandler<ActitoUnknownNotificationOpenedEventArgs> UnknownNotificationOpened;

    event EventHandler<ActitoNotificationActionOpenedEventArgs> NotificationActionOpened;

    event EventHandler<ActitoUnknownNotificationActionOpenedEventArgs> UnknownNotificationActionOpened;

    event EventHandler<ActitoNotificationSettingsChangedEventArgs> NotificationSettingsChanged;

    event EventHandler<ActitoPushSubscriptionChangedEventArgs> SubscriptionChanged;

#if IOS
    event EventHandler<ActitoShouldOpenNotificationSettingsEventArgs> ShouldOpenNotificationSettings;
#endif

    bool HasRemoteNotificationsEnabled { get; }

    ActitoTransport? Transport { get; }

    ActitoPushSubscription? Subscription { get; }

    bool AllowedUI { get; }

#if ANDROID
    bool HandleTrampolineIntent(global::Android.Content.Intent intent);
#endif

    Task EnableRemoteNotificationsAsync();

    Task DisableRemoteNotificationsAsync();

    void SetAuthorizationOptions(IList<ActitoAuthorizationOptions> authorizationOptions);

    void SetCategoryOptions(IList<ActitoCategoryOptions> categoryOptions);

    void SetPresentationOptions(IList<ActitoPresentationOptions> presentationOptions);

#if IOS
    void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken);

    void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error);

    void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    );

    void WillPresentNotification(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification notification,
        Action<UserNotifications.UNNotificationPresentationOptions> completionHandler
    );

    void DidReceiveNotificationResponse(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotificationResponse response,
        Action completionHandler
    );

    void OpenSettings(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification? notification
    );
#endif
}
