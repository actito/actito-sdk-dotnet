using ActitoSdk.InAppMessaging.Android.Internal;
using ActitoSdk.InAppMessaging.Core.Events;
using ActitoSdk.InAppMessaging.Core.Internal;
using NativeActito = ActitoSdk.InAppMessaging.Android.Binding.ActitoInAppMessaging;

namespace ActitoSdk.InAppMessaging.Android;

public class ActitoInAppMessagingPlatformAndroid : IActitoInAppMessagingPlatform
{
    private NativeActito.IMessageLifecycleListener? _messageLifecycleListener;

    public void Initialize()
    {
        if (_messageLifecycleListener != null)
        {
            NativeActito.RemoveLifecycleListener(_messageLifecycleListener);
        }

        _messageLifecycleListener = new LifecycleListener(this);
        NativeActito.AddLifecycleListener(_messageLifecycleListener);
    }

    public event EventHandler<ActitoMessagePresentedEventArgs>? MessagePresented;
    public event EventHandler<ActitoMessageFinishedPresentingEventArgs>? MessageFinishedPresenting;
    public event EventHandler<ActitoMessageFailedToPresentEventArgs>? MessageFailedToPresent;
    public event EventHandler<ActitoActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<ActitoActionFailedToExecuteEventArgs>? ActionFailedToExecute;

    public bool HasMessagesSuppressed
    {
        get => NativeActito.HasMessagesSuppressed;
        set => NativeActito.HasMessagesSuppressed = value;
    }

    public void SetMessagesSuppressed(bool suppressed, bool evaluateContext)
    {
        NativeActito.SetMessagesSuppressed(suppressed, evaluateContext);
    }


    private class LifecycleListener : Java.Lang.Object, NativeActito.IMessageLifecycleListener
    {
        private readonly ActitoInAppMessagingPlatformAndroid _platform;

        internal LifecycleListener(ActitoInAppMessagingPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnMessagePresented(
            Binding.Models.ActitoInAppMessage message
        )
        {
            _platform.MessagePresented?.Invoke(
                _platform,
                new ActitoMessagePresentedEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnMessageFinishedPresenting(
            Binding.Models.ActitoInAppMessage message
        )
        {
            _platform.MessageFinishedPresenting?.Invoke(
                _platform,
                new ActitoMessageFinishedPresentingEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnMessageFailedToPresent(
            Binding.Models.ActitoInAppMessage message
        )
        {
            _platform.MessageFailedToPresent?.Invoke(
                _platform,
                new ActitoMessageFailedToPresentEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnActionExecuted(
            Binding.Models.ActitoInAppMessage message,
            Binding.Models.ActitoInAppMessage.Action action
        )
        {
            _platform.ActionExecuted?.Invoke(
                _platform,
                new ActitoActionExecutedEventArgs(
                    NativeConverter.FromNativeMessage(message),
                    NativeConverter.FromNativeMessageAction(action)
                )
            );
        }

        public void OnActionFailedToExecute(
            Binding.Models.ActitoInAppMessage message,
            Binding.Models.ActitoInAppMessage.Action action,
            Java.Lang.Exception? error
        )
        {
            _platform.ActionFailedToExecute?.Invoke(
                _platform,
                new ActitoActionFailedToExecuteEventArgs(
                    NativeConverter.FromNativeMessage(message),
                    NativeConverter.FromNativeMessageAction(action),
                    error?.ToString()
                )
            );
        }
    }
}
