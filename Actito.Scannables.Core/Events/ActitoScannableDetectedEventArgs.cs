using ActitoSdk.Scannables.Core.Models;

namespace ActitoSdk.Scannables.Core.Events;

public class ActitoScannableDetectedEventArgs(ActitoScannable scannable) : EventArgs
{
    public ActitoScannable Scannable { get; } = scannable;
}
