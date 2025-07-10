namespace ActitoSdk.Push.Core.Events;

public class ActitoUnknownNotificationActionOpenedEventArgs(
    IDictionary<string, object> notification,
    string action,
    string? responseText
) : EventArgs
{
    public IDictionary<string, object> Notification { get; } = notification;
    public string Action { get; } = action;
    public string? ResponseText { get; } = responseText;
}
