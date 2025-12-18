using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoActionFailedToExecuteEventArgs(
    ActitoNotification notification,
    ActitoNotificationAction action,
    string? error
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationAction Action { get; } = action;
    public string? Error { get; } = error;
}
