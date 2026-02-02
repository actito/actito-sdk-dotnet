namespace ActitoSdk.Geo.Core.Models;

/// <summary>
/// Represents heading and orientation data captured from a device.
/// </summary>
/// <remarks>
/// An <see cref="ActitoHeading"/> contains compass and motion sensor information that may be
/// used for location-aware features.
/// </remarks>
public class ActitoHeading
{
    /// <summary>
    /// Magnetic heading of the device in degrees.
    /// </summary>
    /// <remarks>
    /// This value is relative to magnetic north.
    /// </remarks
    public double MagneticHeading { get; }

    /// <summary>
    /// True heading of the device in degrees.
    /// </summary>
    /// <remarks>
    /// This value is relative to true north.
    /// </remarks
    public double TrueHeading { get; }

    /// <summary>
    /// Estimated accuracy of the heading measurement in degrees.
    /// </summary>
    public double HeadingAccuracy { get; }

    /// <summary>
    /// X-axis component of the device's orientation vector.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Y-axis component of the device's orientation vector.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Z-axis component of the device's orientation vector.
    /// </summary>
    public double Z { get; }

    /// <summary>
    /// Timestamp indicating when the heading data was recorded.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoHeading"/>.
    /// </summary>
    public ActitoHeading(double magneticHeading, double trueHeading, double headingAccuracy, double x, double y,
        double z, DateTime timestamp)
    {
        MagneticHeading = magneticHeading;
        TrueHeading = trueHeading;
        HeadingAccuracy = headingAccuracy;
        X = x;
        Y = y;
        Z = z;
        Timestamp = timestamp;
    }
}
