using ActitoSdk.Core.Models;

namespace ActitoSdk.Core.Events;

public class ActitoReadyEventArgs(ActitoApplication application) : EventArgs
{
    public ActitoApplication Application { get; } = application;
}
