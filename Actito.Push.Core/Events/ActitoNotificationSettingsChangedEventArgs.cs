using ActitoSdk.Core.Models;
using ActitoSdk.Push.Core.Models;

namespace ActitoSdk.Push.Core.Events;

public class ActitoNotificationSettingsChangedEventArgs(
    bool allowedUI
) : EventArgs
{
    public bool AllowedUI { get; } = allowedUI;
}
