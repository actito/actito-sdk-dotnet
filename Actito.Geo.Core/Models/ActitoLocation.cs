namespace ActitoSdk.Geo.Core.Models;

/// <summary>
/// Represents a geographic location captured from a device.
/// </summary>
/// <remarks>
/// An <see cref="ActitoLocation"/> contains latitude, longitude, altitude, movement, and
/// accuracy information, along with a timestamp indicating when the location was
/// recorded.
/// </remarks>
public class ActitoLocation
{
    /// <summary>
    /// Latitude of the location in decimal degrees.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Longitude of the location in decimal degrees.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Altitude of the location in meters above sea level.
    /// </summary>
    public double Altitude { get; }

    /// <summary>
    /// Direction of travel in degrees relative to true north.
    /// </summary>
    /// <remarks>
    /// This value represents the device's course of movement.
    /// </remarks>
    public double Course { get; }

    /// <summary>
    /// Speed of the device in meters per second.
    /// </summary>
    public double Speed { get; }

    /// <summary>
    /// Optional floor level of the location.
    /// </summary>
    /// <remarks>
    /// This is typically used for indoor positioning systems.
    /// </remarks>
    public int? Floor { get; }

    /// <summary>
    /// Horizontal accuracy of the location measurement in meters.
    /// </summary>
    public double HorizontalAccuracy { get; }

    /// <summary>
    /// Vertical accuracy of the location measurement in meters.
    /// </summary>
    public double VerticalAccuracy { get; }

    /// <summary>
    /// Timestamp indicating when the location was recorded.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoLocation"/>.
    /// </summary>
    public ActitoLocation(double latitude, double longitude, double altitude, double course, double speed,
        int? floor, double horizontalAccuracy, double verticalAccuracy, DateTime timestamp)
    {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
        Course = course;
        Speed = speed;
        Floor = floor;
        HorizontalAccuracy = horizontalAccuracy;
        VerticalAccuracy = verticalAccuracy;
        Timestamp = timestamp;
    }
}
