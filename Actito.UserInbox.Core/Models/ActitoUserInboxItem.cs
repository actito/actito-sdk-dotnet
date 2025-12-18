using ActitoSdk.Core.Models;

namespace ActitoSdk.UserInbox.Core.Models;

public class ActitoUserInboxItem
{
    public string Id { get; }
    public ActitoNotification Notification { get; }
    public DateTime Time { get; }
    public bool Opened { get; }
    public DateTime? Expires { get; }

    public ActitoUserInboxItem(string id, ActitoNotification notification, DateTime time, bool opened,
        DateTime? expires)
    {
        Id = id;
        Notification = notification;
        Time = time;
        Opened = opened;
        Expires = expires;
    }
}
