using ActitoSdk.Core.Models;

namespace ActitoSdk.Inbox.Core.Models;

public class ActitoInboxItem
{
    public string Id { get; }
    public ActitoNotification Notification { get; }
    public DateTime Time { get; }
    public bool Opened { get; }
    public DateTime? Expires { get; }

    public ActitoInboxItem(string id, ActitoNotification notification, DateTime time, bool opened,
        DateTime? expires)
    {
        Id = id;
        Notification = notification;
        Time = time;
        Opened = opened;
        Expires = expires;
    }
}
