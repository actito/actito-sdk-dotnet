namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// Identifies the transport mechanism used to deliver a notification.
/// </summary>
/// <remarks>
/// This value indicates the underlying push delivery service used by Actito,
/// such as APNS for iOS or GCM for Android.
/// </remarks>
public enum ActitoTransport
{
    /// <summary>
    /// Temporary transport used for a registered device without
    /// remote notifications enabled, before APNS or GCM is available.
    /// </summary>
    Notificare,

    /// <summary>
    /// Google Cloud Messaging.
    /// </summary>
    GCM,

    /// <summary>
    /// Apple Push Notification Service.
    /// </summary>
    APNS,
}

/// <summary>
/// Provides extension methods for <see cref="ActitoTransport"/>.
/// </summary>
public static class ActitoTransportExtensions
{
    /// <summary>
    /// Converts the transport to its raw string representation.
    /// </summary>
    /// <param name="transport">
    /// The transport to convert.
    /// </param>
    /// <returns>
    /// The raw string value corresponding to the transport.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the transport is not recognized.
    /// </exception>
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
