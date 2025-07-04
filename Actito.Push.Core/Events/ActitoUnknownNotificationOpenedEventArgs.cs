namespace ActitoSdk.Push.Core.Events;

public class ActitoUnknownNotificationOpenedEventArgs(
    IDictionary<string, object> notification
) : EventArgs
{
    public IDictionary<string, object> Notification { get; } = notification;
}
