using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoActionExecutedEventArgs(
    ActitoNotification notification,
    ActitoNotificationAction action
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationAction Action { get; } = action;
}
