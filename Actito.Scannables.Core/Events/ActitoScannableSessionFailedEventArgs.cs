namespace ActitoSdk.Scannables.Core.Events;

public class ActitoScannableSessionFailedEventArgs(string? error) : EventArgs
{
    public string? Error { get; } = error;
}
