namespace ActitoSdk.Geo.Core.Models;

/// <summary>
/// Represents a recorded visit or stay at a specific location.
/// </summary>
/// <remarks>
/// An <see cref="ActitoVisit"/> captures the geographic coordinates of the location along
/// with the arrival and departure timestamps. This is typically used for location
/// tracking, analytics, or region-based engagement.
/// </remarks>
public class ActitoVisit
{
    /// <summary>
    /// Timestamp when the visit ended.
    /// </summary>
    public DateTime DepartureDate { get; }

    /// <summary>
    /// Timestamp when the visit started.
    /// </summary>
    public DateTime ArrivalDate { get; }

    /// <summary>
    /// Latitude of the visited location in decimal degrees.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Longitude of the visited location in decimal degrees.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoVisit"/>.
    /// </summary>
    public ActitoVisit(DateTime departureDate, DateTime arrivalDate, double latitude, double longitude)
    {
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Latitude = latitude;
        Longitude = longitude;
    }
}
