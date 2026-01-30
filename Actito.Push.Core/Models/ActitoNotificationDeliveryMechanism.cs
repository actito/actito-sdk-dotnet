namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// Indicates the delivery mechanism of a notification.
/// </summary>
/// <remarks>
/// This enum is used to describe how a notification was delivered to the device.
/// </remarks>
public enum ActitoNotificationDeliveryMechanism
{
    /// <summary>
    /// The notification is displayed normally to the user, with
    /// alerts, sounds, or badges as configured.
    /// </summary>
    Standard,

    /// <summary>
    /// The notification is delivered silently without alerting the user.
    /// </summary>
    Silent,
}

/// <summary>
/// Provides extension methods for <see cref="ActitoNotificationDeliveryMechanism"/>.
/// </summary>
public static class ActitoNotificationDeliveryMechanismExtensions
{
    /// <summary>
    /// Converts the delivery mechanism to its raw string representation.
    /// </summary>
    /// <param name="deliveryMechanism">
    /// The delivery mechanism to convert.
    /// </param>
    /// <returns>
    /// The raw string value corresponding to the delivery mechanism.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the delivery mechanism is not recognized.
    /// </exception>
    public static string ToRawValue(this ActitoNotificationDeliveryMechanism deliveryMechanism)
    {
        return deliveryMechanism switch
        {
            ActitoNotificationDeliveryMechanism.Standard => "standard",
            ActitoNotificationDeliveryMechanism.Silent => "silent",
            _ => throw new ArgumentException(
                $"Unknown {nameof(ActitoNotificationDeliveryMechanism)}: {deliveryMechanism}"
            )
        };
    }
}
