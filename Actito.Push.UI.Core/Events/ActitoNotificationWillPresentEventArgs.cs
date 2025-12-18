using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoNotificationWillPresentEventArgs(ActitoNotification notification) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
}
