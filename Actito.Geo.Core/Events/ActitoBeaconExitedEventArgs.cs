using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoBeaconExitedEventArgs(ActitoBeacon beacon) : EventArgs
{
    public ActitoBeacon Beacon { get; } = beacon;
}
