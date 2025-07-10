using ActitoSdk.Core.Models;
using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoNotificationReceivedEventArgs(
    ActitoNotification notification,
    ActitoNotificationDeliveryMechanism deliveryMechanism
) : EventArgs
{
    public ActitoNotification Notification { get; } = notification;
    public ActitoNotificationDeliveryMechanism DeliveryMechanism { get; } = deliveryMechanism;
}
