using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Events;

public class ActitoVisitEventArgs(ActitoVisit visit) : EventArgs
{
    public ActitoVisit Visit { get; } = visit;
}
