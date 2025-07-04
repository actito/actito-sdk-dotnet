using ActitoSdk.InAppMessaging.Core.Models;

namespace ActitoSdk.InAppMessaging.Core.Events;

public class ActitoActionFailedToExecuteEventArgs(
    ActitoInAppMessage message,
    ActitoInAppMessageAction action,
    string? error
) : EventArgs
{
    public ActitoInAppMessage Message { get; } = message;
    public ActitoInAppMessageAction Action { get; } = action;
    public string? Error { get; } = error;
}
