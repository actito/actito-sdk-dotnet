namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// Represents a push notification subscription for a device.
/// </summary>
/// <remarks>
/// An <see cref="ActitoPushSubscription"/> stores the push token that allows Actito to send
/// push notifications to the device.
/// </remarks>
public class ActitoPushSubscription
{
    /// <summary>
    /// Device push token used to receive notifications.
    /// </summary>
    /// <remarks>
    /// This may be null if the device has not yet registered for push notifications.
    /// </remarks>
    public string? Token { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoPushSubscription"/>.
    /// </summary>
    public ActitoPushSubscription(string? token)
    {
        Token = token;
    }
}
