using ActitoSdk.InAppMessaging.Core.Models;

namespace ActitoSdk.InAppMessaging.Core.Events;

public class ActitoMessagePresentedEventArgs(ActitoInAppMessage message) : EventArgs
{
    public ActitoInAppMessage Message { get; } = message;
}
