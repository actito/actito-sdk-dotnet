using ActitoSdk.iOS.Internal;
using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoTransport? FromNativeTransport(
        ActitoSdk.Push.iOS.Binding.ActitoTransport transport)
    {
        switch (transport)
        {
            case iOS.Binding.ActitoTransport.Notificare:
                return ActitoTransport.Notificare;
            case iOS.Binding.ActitoTransport.Apns:
                return ActitoTransport.APNS;
            case iOS.Binding.ActitoTransport.Unknown:
                return null;
            default:
                throw new ArgumentException($"Unknown transport: {transport}");
        }
    }

    internal static ActitoNotificationDeliveryMechanism FromNativeDeliveryMechanism(
        ActitoSdk.Push.iOS.Binding.ActitoNotificationDeliveryMechanism deliveryMechanism)
    {
        switch (deliveryMechanism)
        {
            case iOS.Binding.ActitoNotificationDeliveryMechanism.Standard:
                return ActitoNotificationDeliveryMechanism.Standard;
            case iOS.Binding.ActitoNotificationDeliveryMechanism.Silent:
                return ActitoNotificationDeliveryMechanism.Silent;
            default:
                throw new ArgumentException($"Unknown notification delivery mechanism: {deliveryMechanism}");
        }
    }

    internal static ActitoPushSubscription FromNativeSubscription(
        ActitoSdk.Push.iOS.Binding.ActitoPushSubscription subscription)
    {
        return new ActitoPushSubscription(
            token: subscription.Token
        );
    }

    internal static ActitoSystemNotification FromNativeSystemNotification(
        ActitoSdk.Push.iOS.Binding.ActitoSystemNotification notification)
    {
        return new ActitoSystemNotification(
            id: notification.Id.ToString(),
            type: notification.Type.ToString(),
            extra: ActitoNativeConverter.FromNativeExtraDictionary(notification.Extra).ToDictionary(
                kvp => kvp.Key,
                object? (kvp) => kvp.Value
            )
        );
    }
}
