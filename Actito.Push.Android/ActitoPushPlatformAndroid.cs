using Android.Content;
using AndroidX.Lifecycle;
using ActitoSdk.Android.Internal;
using ActitoSdk.Push.Android.Internal;
using ActitoSdk.Push.Core.Events;
using ActitoSdk.Push.Core.Models;
using ActitoSdk.Push.Core.Internal;
using NativeActito = ActitoSdk.Push.Android.Binding.ActitoPushCompat;

namespace ActitoSdk.Push.Android;

public class ActitoPushPlatformAndroid : IActitoPushPlatform
{
    private IObserver? _allowedUIObserver;
    private IObserver? _subscriptionObserver;

    public void Initialize()
    {
        ActitoDotNetPushIntentReceiver.Platform = this;
        NativeActito.IntentReceiver = Java.Lang.Class.FromType(typeof(ActitoDotNetPushIntentReceiver));

        ObserveAllowedUI();
        ObserveSubscription();
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

    public bool HasRemoteNotificationsEnabled => NativeActito.HasRemoteNotificationsEnabled;

    public ActitoTransport? Transport
    {
        get
        {
            var transport = NativeActito.Transport;
            return transport == null ? null : NativeConverter.FromNativeTransport(transport);
        }
    }

    public ActitoPushSubscription? Subscription
    {
        get
        {
            var subscription = NativeActito.Subscription;
            return subscription == null ? null : NativeConverter.FromNativeSubscription(subscription);
        }
    }

    public bool AllowedUI => NativeActito.AllowedUI;

    public bool HandleTrampolineIntent(Intent intent)
    {
        return NativeActito.HandleTrampolineIntent(intent);
    }

    public async Task EnableRemoteNotificationsAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.EnableRemoteNotifications(callback);

        await callback.Task;
    }

    public async Task DisableRemoteNotificationsAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.DisableRemoteNotifications(callback);

        await callback.Task;
    }

    public void SetAuthorizationOptions(IList<ActitoAuthorizationOptions> authorizationOptions)
    {
        // no-op
    }

    public void SetCategoryOptions(IList<ActitoCategoryOptions> categoryOptions)
    {
        // no-op
    }

    public void SetPresentationOptions(IList<ActitoPresentationOptions> presentationOptions)
    {
        // no-op
    }

    private void ObserveAllowedUI()
    {
        if (_allowedUIObserver != null)
        {
            NativeActito.ObservableAllowedUI.RemoveObserver(_allowedUIObserver);
        }

        _allowedUIObserver = new AllowedUIObserver(this);
        NativeActito.ObservableAllowedUI.ObserveForever(_allowedUIObserver);
    }

    private void ObserveSubscription()
    {
        if (_subscriptionObserver != null)
        {
            NativeActito.ObservableSubscription.RemoveObserver(_subscriptionObserver);
        }

        _subscriptionObserver = new SubscriptionObserver(this);
        NativeActito.ObservableSubscription.ObserveForever(_subscriptionObserver);
    }


    private class AllowedUIObserver : Java.Lang.Object, IObserver
    {
        private readonly ActitoPushPlatformAndroid _platform;

        internal AllowedUIObserver(ActitoPushPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value == null) return;

            _platform.NotificationSettingsChanged?.Invoke(
                _platform,
                new ActitoNotificationSettingsChangedEventArgs(
                    allowedUI: value == Java.Lang.Boolean.True
                )
            );
        }
    }

    private class SubscriptionObserver : Java.Lang.Object, IObserver
    {
        private readonly ActitoPushPlatformAndroid _platform;

        internal SubscriptionObserver(ActitoPushPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value == null)
            {
                _platform.SubscriptionChanged?.Invoke(
                    _platform,
                    new ActitoPushSubscriptionChangedEventArgs(
                        subscription: null
                    )
                );

                return;
            }

            if (value is not Binding.Models.ActitoPushSubscription subscription)
            {
                _platform.SubscriptionChanged?.Invoke(
                    _platform,
                    new ActitoPushSubscriptionChangedEventArgs(
                        subscription: null
                    )
                );

                return;
            }

            _platform.SubscriptionChanged?.Invoke(
                _platform,
                new ActitoPushSubscriptionChangedEventArgs(
                    subscription: NativeConverter.FromNativeSubscription(subscription)
                )
            );
        }
    }

    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class ActitoDotNetPushIntentReceiver : Binding.ActitoPushIntentReceiver
    {
        internal static ActitoPushPlatformAndroid? Platform;

        protected override void OnNotificationReceived(
            Context context,
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            Binding.Models.ActitoNotificationDeliveryMechanism deliveryMechanism)
        {
            Platform?.NotificationReceived?.Invoke(
                this,
                new ActitoNotificationReceivedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification),
                    deliveryMechanism: NativeConverter.FromNativeDeliveryMechanism(deliveryMechanism)
                )
            );
        }

        protected override void OnSystemNotificationReceived(
            Context context,
            Binding.Models.ActitoSystemNotification notification)
        {
            Platform?.SystemNotificationReceived?.Invoke(
                this,
                new ActitoSystemNotificationReceivedEventArgs(
                    notification: NativeConverter.FromNativeSystemNotification(notification)
                )
            );
        }

        protected override void OnUnknownNotificationReceived(
            Context context,
            Binding.Models.ActitoUnknownNotification notification)
        {
            Platform?.UnknownNotificationReceived?.Invoke(
                this,
                new ActitoUnknownNotificationReceivedEventArgs(
                    notification: NativeConverter.FromNativeUnknownNotification(notification)
                )
            );
        }

        protected override void OnNotificationOpened(
            Context context,
            ActitoSdk.Android.Binding.Models.ActitoNotification notification)
        {
            Platform?.NotificationOpened?.Invoke(
                this,
                new ActitoNotificationOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        protected override void OnActionOpened(
            Context context,
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            ActitoSdk.Android.Binding.Models.ActitoNotification.Action action)
        {
            Platform?.NotificationActionOpened?.Invoke(
                this,
                new ActitoNotificationActionOpenedEventArgs(
                    notification: ActitoNativeConverter.FromNativeNotification(notification),
                    action: ActitoNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }
    }
}
