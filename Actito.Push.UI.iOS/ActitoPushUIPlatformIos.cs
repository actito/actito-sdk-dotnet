using ActitoSdk.Core.Models;
using ActitoSdk.iOS.Internal;
using ActitoSdk.Push.UI.Core.Events;
using ActitoSdk.Push.UI.Core.Internal;

namespace ActitoSdk.Push.UI.iOS;

public class ActitoPushUIPlatformIos : IActitoPushUIPlatform
{
    private InternalActitoPushUIDelegate? _delegate;
    private Binding.ActitoPushUINativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoPushUIDelegate(this);
        _native.Delegate = _delegate;
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
        UIViewController controller
    )
    {
        _native.PresentNotification(
            notification: ActitoNativeConverter.ToNativeNotification(notification),
            controller: controller
        );
    }

    public void PresentAction(
        ActitoNotification notification,
        ActitoNotificationAction action,
        UIViewController controller
    )
    {
        _native.PresentAction(
            notification: ActitoNativeConverter.ToNativeNotification(notification),
            action: ActitoNativeConverter.ToNativeNotificationAction(action),
            controller: controller
        );
    }

    public bool RequiresViewController(ActitoNotification notification)
    {
        return _native.RequiresViewController(ActitoNativeConverter.ToNativeNotification(notification));
    }


    private sealed class InternalActitoPushUIDelegate : Binding.ActitoPushUINativeBindingDelegate
    {
        private readonly ActitoPushUIPlatformIos _platform;

        internal InternalActitoPushUIDelegate(ActitoPushUIPlatformIos platform)
        {
            _platform = platform;
        }

        public override void WillPresentNotification(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationWillPresent?.Invoke(
                _platform,
                new ActitoNotificationWillPresentEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidPresentNotification(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationPresented?.Invoke(
                _platform,
                new ActitoNotificationPresentedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidFinishPresentingNotification(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationFinishedPresenting?.Invoke(
                _platform,
                new ActitoNotificationFinishedPresentingEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidFailToPresentNotification(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.NotificationFailedToPresent?.Invoke(
                _platform,
                new ActitoNotificationFailedToPresentEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidClickURL(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            NSUrl url,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            var urlStr = url.AbsoluteString;
            if (urlStr == null) return;

            _platform.NotificationUrlClicked?.Invoke(
                _platform,
                new ActitoNotificationUrlClickedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    urlStr
                )
            );
        }

        public override void WillExecuteAction(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification
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

        public override void DidExecuteAction(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification
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

        public override void DidNotExecuteAction(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            _platform.ActionNotExecuted?.Invoke(
                _platform,
                new ActitoActionNotExecutedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public override void DidFailToExecuteAction(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification,
            NSError? error
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

        public override void DidReceiveCustomAction(
            Binding.ActitoPushUINativeBinding actitoPushUI,
            NSUrl url,
            ActitoSdk.iOS.Binding.ActitoNotificationAction action,
            ActitoSdk.iOS.Binding.ActitoNotification notification
        )
        {
            var urlStr = url.AbsoluteString;
            if (urlStr == null) return;

            _platform.CustomActionReceived?.Invoke(
                _platform,
                new ActitoCustomActionReceivedEventArgs(
                    ActitoNativeConverter.FromNativeNotification(notification),
                    ActitoNativeConverter.FromNativeNotificationAction(action),
                    urlStr
                )
            );
        }
    }
}
