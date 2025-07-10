using ActitoSdk.Core.Models;

namespace ActitoSdk.Core.Events;

public class ActitoDeviceRegisteredEventArgs(ActitoDevice device) : EventArgs
{
    public ActitoDevice Device { get; } = device;
}
