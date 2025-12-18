using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoBeaconEnteredEventArgs(ActitoBeacon beacon) : EventArgs
{
    public ActitoBeacon Beacon { get; } = beacon;
}
