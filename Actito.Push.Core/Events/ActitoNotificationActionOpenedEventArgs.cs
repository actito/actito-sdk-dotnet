using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoNotificationActionOpenedEventArgs(
    ActitoNotification notification,
    ActitoNotificationAction action
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationAction Action { get; } = action;
}
