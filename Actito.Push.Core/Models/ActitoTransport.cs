namespace ActitoSdk.Push.Core.Models;

public enum ActitoTransport
{
    Notificare,
    GCM,
    APNS,
}

public static class ActitoTransportExtensions
{
    public static string ToRawValue(this ActitoTransport transport)
    {
        return transport switch
        {
            ActitoTransport.Notificare => "Notificare",
            ActitoTransport.GCM => "GCM",
            ActitoTransport.APNS => "APNS",
            _ => throw new ArgumentException($"Unknown {nameof(ActitoTransport)}: {transport}")
        };
    }
}
