namespace ActitoSdk.Push.Core.Models;

public enum ActitoNotificationDeliveryMechanism
{
    Standard,
    Silent,
}

public static class ActitoNotificationDeliveryMechanismExtensions
{
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
