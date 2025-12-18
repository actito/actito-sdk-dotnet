using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoLocationUpdatedEventArgs(ActitoLocation location) : EventArgs
{
    public ActitoLocation Location { get; } = location;
}
