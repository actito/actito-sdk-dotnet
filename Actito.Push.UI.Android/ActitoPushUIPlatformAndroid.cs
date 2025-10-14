using ActitoSdk.Android.Internal;
using ActitoSdk.Core.Models;
using ActitoSdk.Push.UI.Core.Events;
using ActitoSdk.Push.UI.Core.Internal;
using NativeActito = ActitoSdk.Push.UI.Android.Binding.ActitoPushUI;

namespace ActitoSdk.Push.UI.Android;

public class ActitoPushUIPlatformAndroid : IActitoPushUIPlatform
{
    private NativeActito.INotificationLifecycleListener? _notificationLifecycleListener;

    public void Initialize()
    {
        ObserveNotificationLifecycleEvents();
    }

    public event EventHandler<ActitoNotificationWillPresentEventArgs>? NotificationWillPresent;
    public event EventHandler<ActitoNotificationPresentedEventArgs>? NotificationPresented;
    public event EventHandler<ActitoNotificationFinishedPresentingEventArgs>? NotificationFinishedPresenting;
    public event EventHandler<ActitoNotificationFailedToPresentEventArgs>? NotificationFailedToPresent;
    public event EventHandler<ActitoNotificationUrlClickedEventArgs>? NotificationUrlClicked;
    public event EventHandler<ActitoActionWillExecuteEventArgs>? ActionWillExecute;
    public event EventHandler<ActitoActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<ActitoActionNotExecutedEventArgs>? ActionNotExecuted;
    public event EventHandler<ActitoActionFailedToExecuteEventArgs>? ActionFailedToExecute;
    public event EventHandler<ActitoCustomActionReceivedEventArgs>? CustomActionReceived;

    public void PresentNotification(
        ActitoNotification notification,
        Activity activity
    )
    {
        NativeActito.PresentNotification(
            notification: ActitoNativeConverter.ToNativeNotification(notification),
            activity: activity
        );
    }

    public void PresentAction(
        ActitoNotification notification,
        ActitoNotificationAction action,
        Activity activity
    )
    {
        NativeActito.PresentAction(
            notification: ActitoNativeConverter.ToNativeNotification(notification),
            action: ActitoNativeConverter.ToNativeNotificationAction(action),
            activity: activity
        );
    }

    private void ObserveNotificationLifecycleEvents()
    {
        if (_notificationLifecycleListener != null)
        {
            NativeActito.RemoveLifecycleListener(_notificationLifecycleListener);
        }

        _notificationLifecycleListener = new NotificationLifecycleListener(this);
        NativeActito.AddLifecycleListener(_notificationLifecycleListener);
    }

    private class NotificationLifecycleListener : Java.Lang.Object,
        NativeActito.INotificationLifecycleListener
    {
        private readonly ActitoPushUIPlatformAndroid _platform;

        internal NotificationLifecycleListener(ActitoPushUIPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnNotificationWillPresent(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification
        )
        {
            _platform.NotificationWillPresent?.Invoke(
                _platform,
                new ActitoNotificationWillPresentEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationPresented(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification
        )
        {
            _platform.NotificationPresented?.Invoke(
                _platform,
                new ActitoNotificationPresentedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationFinishedPresenting(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification
        )
        {
            _platform.NotificationFinishedPresenting?.Invoke(
                _platform,
                new ActitoNotificationFinishedPresentingEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationFailedToPresent(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification
        )
        {
            _platform.NotificationFailedToPresent?.Invoke(
                _platform,
                new ActitoNotificationFailedToPresentEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationUrlClicked(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            global::Android.Net.Uri uri
        )
        {
            var url = uri.ToString();
            if (url == null) return;

            _platform.NotificationUrlClicked?.Invoke(
                _platform,
                new ActitoNotificationUrlClickedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    url
                )
            );
        }

        public void OnActionWillExecute(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            ActitoSdk.Android.Binding.Models.ActitoNotification.Action action
        )
        {
            _platform.ActionWillExecute?.Invoke(
                _platform,
                new ActitoActionWillExecuteEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public void OnActionExecuted(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            ActitoSdk.Android.Binding.Models.ActitoNotification.Action action
        )
        {
            _platform.ActionExecuted?.Invoke(
                _platform,
                new ActitoActionExecutedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public void OnActionFailedToExecute(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            ActitoSdk.Android.Binding.Models.ActitoNotification.Action action,
            Java.Lang.Exception? error
        )
        {
            _platform.ActionFailedToExecute?.Invoke(
                _platform,
                new ActitoActionFailedToExecuteEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action),
                    error?.ToString()
                )
            );
        }

        public void OnCustomActionReceived(
            ActitoSdk.Android.Binding.Models.ActitoNotification notification,
            ActitoSdk.Android.Binding.Models.ActitoNotification.Action action,
            global::Android.Net.Uri uri
        )
        {
            var url = uri.ToString();
            if (url == null) return;

            _platform.CustomActionReceived?.Invoke(
                _platform,
                new ActitoCustomActionReceivedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action),
                    url
                )
            );
        }
    }
}
