namespace ActitoSdk.Core.Models;

/// <summary>
/// Represents a device registered in Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoDevice"/> is associated with a physical device and may be optionally
/// linked to a user. It contains timezone information, user-related metadata,
/// and optional configuration such as do-not-disturb settings.
/// </remarks>
public class ActitoDevice
{
    /// <summary>
    /// Unique identifier of the device.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Optional identifier of the user associated with the device.
    /// </summary>
    public string? UserId { get; }

    /// <summary>
    /// Optional display name of the associated user.
    /// </summary>
    public string? UserName { get; }

    /// <summary>
    /// ime zone offset of the device in hours relative to UTC.
    /// </summary>
    public double TimeZoneOffset { get; }

    /// <summary>
    /// Optional <see cref="ActitoDoNotDisturb"/> configuration for the device.
    /// </summary>
    public ActitoDoNotDisturb? Dnd { get; }

    /// <summary>
    /// Custom user data associated with the device.
    /// </summary>
    /// <remarks>
    /// This map contains key–value pairs representing user attributes or profile
    /// information linked to the device.
    /// </remarks>
    public IDictionary<string, string> UserData { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoDevice"/>.
    /// </summary>
    public ActitoDevice(string id, string? userId, string? userName, double timeZoneOffset, ActitoDoNotDisturb? dnd, IDictionary<string, string> userData)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        TimeZoneOffset = timeZoneOffset;
        Dnd = dnd;
        UserData = userData;
    }
}
