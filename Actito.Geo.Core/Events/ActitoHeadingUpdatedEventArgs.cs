using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoHeadingUpdatedEventArgs(ActitoHeading heading) : EventArgs
{
    public ActitoHeading Heading { get; } = heading;
}
