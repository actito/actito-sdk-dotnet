using ActitoSdk.Core.Models;

namespace ActitoSdk.UserInbox.Core.Models;

/// <summary>
/// Represents an item in the Actito user inbox.
/// </summary>
/// <remarks>
/// An <see cref="ActitoUserInboxItem"/> contains a notification and metadata about its
/// read state within the inbox. Inbox items can optionally have an expiration date.
/// </remarks>
public class ActitoUserInboxItem
{
    /// <summary>
    /// Unique identifier of the inbox item.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Notification associated with this inbox item.
    /// </summary>
    public ActitoNotification Notification { get; }

    /// <summary>
    /// Timestamp indicating when the item was received.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Indicates whether the item has been opened by the user.
    /// </summary>
    public bool Opened { get; }

    /// <summary>
    /// Optional expiration timestamp of the item.
    /// </summary>
    public DateTime? Expires { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoUserInboxItem"/>.
    /// </summary>
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
