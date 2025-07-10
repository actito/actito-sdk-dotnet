using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.Core.Internal;
using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push;

public static class ActitoPush
{
    private static readonly Lazy<IActitoPushPlatform> Implementation = new(() =>
    {
        var instance = CreateActito();
        instance.Initialize();

        return instance;
    });

    private static IActitoPushPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    /// <summary>
    /// Called when a push notification is received.
    /// </summary>
    public static event EventHandler<ActitoNotificationReceivedEventArgs> NotificationReceived
    {
        add => Platform.NotificationReceived += value;
        remove => Platform.NotificationReceived -= value;
    }

    /// <summary>
    /// Called when a custom system notification is received.
    /// </summary>
    public static event EventHandler<ActitoSystemNotificationReceivedEventArgs> SystemNotificationReceived
    {
        add => Platform.SystemNotificationReceived += value;
        remove => Platform.SystemNotificationReceived -= value;
    }

    /// <summary>
    /// Called when an unknown notification is received.
    /// </summary>
    public static event EventHandler<ActitoUnknownNotificationReceivedEventArgs> UnknownNotificationReceived
    {
        add => Platform.UnknownNotificationReceived += value;
        remove => Platform.UnknownNotificationReceived -= value;
    }

    /// <summary>
    /// Called when a push notification is opened by the user.
    /// </summary>
    public static event EventHandler<ActitoNotificationOpenedEventArgs> NotificationOpened
    {
        add => Platform.NotificationOpened += value;
        remove => Platform.NotificationOpened -= value;
    }

    /// <summary>
    /// Called when an unknown push notification is opened by the user.
    /// </summary>
    public static event EventHandler<ActitoUnknownNotificationOpenedEventArgs> UnknownNotificationOpened
    {
        add => Platform.UnknownNotificationOpened += value;
        remove => Platform.UnknownNotificationOpened -= value;
    }

    /// <summary>
    /// Called when a push notification action is opened by the user.
    /// </summary>
    public static event EventHandler<ActitoNotificationActionOpenedEventArgs> NotificationActionOpened
    {
        add => Platform.NotificationActionOpened += value;
        remove => Platform.NotificationActionOpened -= value;
    }

    /// <summary>
    /// Called when an unknown push notification action is opened by the user.
    /// </summary>
    public static event EventHandler<ActitoUnknownNotificationActionOpenedEventArgs> UnknownNotificationActionOpened
    {
        add => Platform.UnknownNotificationActionOpened += value;
        remove => Platform.UnknownNotificationActionOpened -= value;
    }

    /// <summary>
    /// Called when the notification settings are changed.
    /// </summary>
    public static event EventHandler<ActitoNotificationSettingsChangedEventArgs> NotificationSettingsChanged
    {
        add => Platform.NotificationSettingsChanged += value;
        remove => Platform.NotificationSettingsChanged -= value;
    }

    /// <summary>
    /// Called when the device's push subscription changes.
    /// </summary>
    public static event EventHandler<ActitoPushSubscriptionChangedEventArgs> SubscriptionChanged
    {
        add => Platform.SubscriptionChanged += value;
        remove => Platform.SubscriptionChanged -= value;
    }

#if IOS
    /// <summary>
    /// Called when a notification prompts the app to open its settings screen.
    /// </summary>
    public static event EventHandler<ActitoShouldOpenNotificationSettingsEventArgs> ShouldOpenNotificationSettings
    {
        add => Platform.ShouldOpenNotificationSettings += value;
        remove => Platform.ShouldOpenNotificationSettings -= value;
    }
#endif


    /// <summary>
    /// Indicates whether remote notifications are enabled.
    /// </summary>
    public static bool HasRemoteNotificationsEnabled => Platform.HasRemoteNotificationsEnabled;

    /// <summary>
    /// Provides the current push transport information.
    /// </summary>
    public static ActitoTransport? Transport => Platform.Transport;

    /// <summary>
    /// Provides the current push subscription.
    /// </summary>
    public static ActitoPushSubscription? Subscription => Platform.Subscription;

    /// <summary>
    /// Indicates whether the device is capable of receiving remote notifications.
    ///
    /// This function returns `true` if the user has granted permission to receive push notifications and the device
    /// has successfully obtained a push token from the notification service. It reflects whether the app can present
    /// notifications as allowed by the system and user settings.
    /// </summary>
    public static bool AllowedUI => Platform.AllowedUI;


#if ANDROID
    /// <summary>
    /// Handles a trampoline intent.
    ///
    /// This method processes an intent and determines if it is a Actito push notification intent
    /// that requires handling by a trampoline mechanism.
    /// </summary>
    /// <param name="intent">The <see cref="global::Android.Content.Intent"/> to handle.</param>
    /// <returns>
    /// <c>true</c> if the intent was handled, <c>false</c> otherwise.
    /// </returns>
    public static bool HandleTrampolineIntent(global::Android.Content.Intent intent) =>
        Platform.HandleTrampolineIntent(intent);
#endif

    /// <summary>
    /// Enables remote notifications.
    ///
    /// This function enables remote notifications for the application, allowing push notifications to be received.
    ///
    /// <strong>Note</strong>: Starting with Android 13 (API level 33), this function requires the developer to
    /// explicitly request the <c>POST_NOTIFICATIONS</c> permission from the user.
    /// </summary>
    /// <returns></returns>
    public static Task EnableRemoteNotificationsAsync() => Platform.EnableRemoteNotificationsAsync();

    /// <summary>
    /// Disables remote notifications.
    ///
    /// This function disables remote notifications for the application, preventing push notifications from being received.
    /// </summary>
    /// <returns></returns>
    public static Task DisableRemoteNotificationsAsync() => Platform.DisableRemoteNotificationsAsync();

    /// <summary>
    /// Defines the authorization options used when requesting push notification permissions.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="authorizationOptions">The authorization options to be set.</param>
    public static void SetAuthorizationOptions(IList<string> authorizationOptions) =>
        Platform.SetAuthorizationOptions(authorizationOptions);

    /// <summary>
    /// Defines the notification category options for custom notification actions.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="categoryOptions">The category options to be set.</param>
    public static void SetCategoryOptions(IList<string> categoryOptions) =>
        Platform.SetCategoryOptions(categoryOptions);

    /// <summary>
    /// Defines the presentation options for displaying notifications while the app is in the foreground.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="presentationOptions">The presentation options to be set.</param>
    public static void SetPresentationOptions(IList<string> presentationOptions) =>
        Platform.SetPresentationOptions(presentationOptions);

#if IOS
    /// <summary>
    /// Called when the app successfully registers with Apple Push Notification Service (APNS).
    /// </summary>
    /// <param name="application">The singleton app instance.</param>
    /// <param name="deviceToken">The device token data for remote notifications.</param>
    public static void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) =>
        Platform.RegisteredForRemoteNotifications(application, deviceToken);

    /// <summary>
    /// Called when the app fails to register for remote notifications.
    /// </summary>
    /// <param name="application">The singleton app instance.</param>
    /// <param name="error">An error object describing why registration failed.</param>
    public static void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error) =>
        Platform.FailedToRegisterForRemoteNotifications(application, error);

    /// <summary>
    /// Called when a remote notification is received. Used to handle notification content and initiate background processing if necessary.
    /// </summary>
    /// <param name="application">The singleton app instance.</param>
    /// <param name="userInfo">The payload of the received remote notification.</param>
    /// <param name="completionHandler">A handler to be called with a <see cref="UIBackgroundFetchResult"/> after processing the notification.</param>
    public static void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    ) => Platform.DidReceiveRemoteNotification(application, userInfo, completionHandler);

    /// <summary>
    /// Called when a notification is delivered to the app while it’s in the foreground.
    /// </summary>
    /// <param name="center">The notification center managing notifications for the app.</param>
    /// <param name="notification">The notification being presented.</param>
    /// <param name="completionHandler">A completion handler to call with the desired presentation options.</param>
    public static void WillPresentNotification(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification notification,
        Action<UserNotifications.UNNotificationPresentationOptions> completionHandler
    ) => Platform.WillPresentNotification(center, notification, completionHandler);

    /// <summary>
    /// Called when the user interacts with a notification.
    /// </summary>
    /// <param name="center">The notification center managing notifications for the app.</param>
    /// <param name="response">The user’s response to the notification.</param>
    /// <param name="completionHandler">A completion handler to call after processing the response.</param>
    public static void DidReceiveNotificationResponse(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotificationResponse response,
        Action completionHandler
    ) => Platform.DidReceiveNotificationResponse(center, response, completionHandler);

    /// <summary>
    /// Called when a notification prompts the app to open its settings screen.
    /// </summary>
    /// <param name="center">The notification center managing notifications for the app.</param>
    /// <param name="notification">The notification that prompted the settings to be opened, if applicable.</param>
    public static void OpenSettings(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification? notification
    ) => Platform.OpenSettings(center, notification);
#endif


    private static IActitoPushPlatform CreateActito()
    {
#if ANDROID
        return new Android.ActitoPushPlatformAndroid();
#elif IOS
        return new iOS.ActitoPushPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Actito.");
    }
}
