using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoNotificationOpenedEventArgs(
    ActitoNotification notification
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
}
