using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoRegionEnteredEventArgs(ActitoRegion region) : EventArgs
{
    public ActitoRegion Region { get; } = region;
}
