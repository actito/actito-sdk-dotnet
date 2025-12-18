using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoNotificationPresentedEventArgs(ActitoNotification notification) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
}
