using ActitoSdk.Core.Models;

namespace ActitoSdk.Inbox.Core.Models;

/// <summary>
/// Represents an item in the Actito inbox.
/// </summary>
/// <remarks>
/// An <see cref="ActitoInboxItem"/> contains a notification and metadata about its read
/// state within the inbox. Inbox items can optionally have an expiration date.
/// </remarks>
public class ActitoInboxItem
{
    /// <summary>
    /// Unique identifier of the inbox item.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// <see cref="ActitoNotification"/> associated with this inbox item.
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
    /// Constructor for <see cref="ActitoInboxItem"/>.
    /// </summary>
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
