using ActitoSdk.InAppMessaging.Core.Events;

namespace ActitoSdk.InAppMessaging.Core.Internal;

public interface IActitoInAppMessagingPlatform
{
    void Initialize();

    event EventHandler<ActitoMessagePresentedEventArgs> MessagePresented;

    event EventHandler<ActitoMessageFinishedPresentingEventArgs> MessageFinishedPresenting;

    event EventHandler<ActitoMessageFailedToPresentEventArgs> MessageFailedToPresent;

    event EventHandler<ActitoActionExecutedEventArgs> ActionExecuted;

    event EventHandler<ActitoActionFailedToExecuteEventArgs> ActionFailedToExecute;

    bool HasMessagesSuppressed { get; set; }

    void SetMessagesSuppressed(bool suppressed, bool evaluateContext);
}
