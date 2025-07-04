using ActitoSdk.InAppMessaging.Core.Models;

namespace ActitoSdk.InAppMessaging.Core.Events;

public class ActitoActionExecutedEventArgs(
    ActitoInAppMessage message,
    ActitoInAppMessageAction action
) : EventArgs
{
    public ActitoInAppMessage Message { get; } = message;
    public ActitoInAppMessageAction Action { get; } = action;
}
