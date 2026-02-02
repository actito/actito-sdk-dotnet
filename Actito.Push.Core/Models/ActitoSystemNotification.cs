namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// Represents a system-level notification sent by Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoSystemNotification"/> contains metadata about system events or updates,
/// distinct from user-targeted notifications. These notifications may include
/// additional information in the [extra] map.
/// </remarks>
public class ActitoSystemNotification
{
    /// <summary>
    /// Unique identifier of the system notification.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Type of the system notification.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Collection of key-value pairs used to add extra information to the notification.
    /// </summary>
    public IDictionary<string, object?> Extra { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoSystemNotification"/>.
    /// </summary>
    public ActitoSystemNotification(string id, string type, IDictionary<string, object?> extra)
    {
        Id = id;
        Type = type;
        Extra = extra;
    }
}
