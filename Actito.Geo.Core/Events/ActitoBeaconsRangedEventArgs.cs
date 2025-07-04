using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoBeaconsRangedEventArgs(ActitoRegion region, IList<ActitoBeacon> beacons) : EventArgs
{
    public ActitoRegion Region { get; } = region;
    public IList<ActitoBeacon> Beacons { get; } = beacons;
}
