using ActitoSdk.Core.Models;

namespace ActitoSdk.Push.UI.Core.Events;

public class ActitoCustomActionReceivedEventArgs(
    ActitoNotification notification,
    ActitoNotificationAction action,
    string url
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationAction Action { get; } = action;
    public string Url { get; } = url;
}
