using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoPushSubscriptionChangedEventArgs(ActitoPushSubscription? subscription) : EventArgs
{
    public ActitoPushSubscription? Subscription { get; } = subscription;
}
