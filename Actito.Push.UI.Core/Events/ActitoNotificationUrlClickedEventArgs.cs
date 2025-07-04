using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoNotificationUrlClickedEventArgs(ActitoNotification notification, string url) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public string Url { get; } = url;
}
