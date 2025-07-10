using ActitoSdk.InAppMessaging.Core.Events;
using ActitoSdk.InAppMessaging.Core.Internal;
using ActitoSdk.InAppMessaging.iOS.Internal;

namespace ActitoSdk.InAppMessaging.iOS;

public class ActitoInAppMessagingPlatformIos : IActitoInAppMessagingPlatform
{
    private InternalActitoInAppMessagingDelegate? _delegate;
    private Binding.ActitoInAppMessagingNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoInAppMessagingDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoMessagePresentedEventArgs>? MessagePresented;
    public event EventHandler<ActitoMessageFinishedPresentingEventArgs>? MessageFinishedPresenting;
    public event EventHandler<ActitoMessageFailedToPresentEventArgs>? MessageFailedToPresent;
    public event EventHandler<ActitoActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<ActitoActionFailedToExecuteEventArgs>? ActionFailedToExecute;

    public bool HasMessagesSuppressed
    {
        get => _native.HasMessagesSuppressed;
        set => _native.HasMessagesSuppressed = value;
    }

    public void SetMessagesSuppressed(bool suppressed, bool evaluateContext)
    {
        _native.SetMessagesSuppressed(suppressed, evaluateContext);
    }


    private sealed class InternalActitoInAppMessagingDelegate : Binding.ActitoInAppMessagingNativeBindingDelegate
    {
        private readonly ActitoInAppMessagingPlatformIos _platform;

        internal InternalActitoInAppMessagingDelegate(ActitoInAppMessagingPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidPresentMessage(
            Binding.ActitoInAppMessagingNativeBinding actito,
            Binding.ActitoInAppMessage message
        )
        {
            _platform.MessagePresented?.Invoke(
                _platform,
                new ActitoMessagePresentedEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidFinishPresentingMessage(
            Binding.ActitoInAppMessagingNativeBinding actito,
            Binding.ActitoInAppMessage message
        )
        {
            _platform.MessageFinishedPresenting?.Invoke(
                _platform,
                new ActitoMessageFinishedPresentingEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidFailToPresentMessage(
            Binding.ActitoInAppMessagingNativeBinding actito,
            Binding.ActitoInAppMessage message
        )
        {
            _platform.MessageFailedToPresent?.Invoke(
                _platform,
                new ActitoMessageFailedToPresentEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidExecuteAction(
            Binding.ActitoInAppMessagingNativeBinding actito,
            Binding.ActitoInAppMessageAction action,
            Binding.ActitoInAppMessage message
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

        public override void DidFailToExecuteAction(
            Binding.ActitoInAppMessagingNativeBinding actito,
            Binding.ActitoInAppMessageAction action,
            Binding.ActitoInAppMessage message,
            NSError? error
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
