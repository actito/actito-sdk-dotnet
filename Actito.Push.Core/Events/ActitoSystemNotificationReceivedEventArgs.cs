using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoSystemNotificationReceivedEventArgs(
    ActitoSystemNotification notification
) : EventArgs
{
    public ActitoSystemNotification Notification { get; } = notification;
}
