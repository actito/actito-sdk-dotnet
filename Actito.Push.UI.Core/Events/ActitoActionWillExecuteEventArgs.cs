using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoActionWillExecuteEventArgs(
    ActitoNotification notification,
    ActitoNotificationAction action
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationAction Action { get; } = action;
}
