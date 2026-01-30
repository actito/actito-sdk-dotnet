namespace ActitoSdk.Geo.Core.Models;

/// <summary>
/// Represents a geographic region configured in Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoRegion"/> defines a location-based area that can be used for proximity
/// detection, geofencing, or region-triggered actions.
/// Regions may be defined using simple or advanced geometries.
/// </remarks>
public class ActitoRegion
{
    /// <summary>
    /// Unique identifier of the region.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Human-readable name of the region.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Optional description of the region.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Optional reference key associated with the region.
    /// </summary>
    public string? ReferenceKey { get; }

    /// <summary>
    /// Primary geometry defining the region.
    /// </summary>
    public ActitoRegionGeometry Geometry { get; }

    /// <summary>
    /// Optional advanced geometry defining complex region shapes.
    /// </summary>
    public ActitoRegionAdvancedGeometry? AdvancedGeometry { get; }

    /// <summary>
    /// Optional major value associated with the region.
    /// </summary>
    /// <remarks>
    /// This is typically used for beacon-based regions.
    /// </remarks>
    public int? Major { get; }

    /// <summary>
    /// Distance from the device to the region in meters.
    /// </summary>
    public double Distance { get; }

    /// <summary>
    /// Time zone identifier associated with the region.
    /// </summary>
    public string TimeZone { get; }

    /// <summary>
    /// Time zone offset of the region in hours relative to UTC.
    /// </summary>
    public double TimeZoneOffset { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoRegion"/>.
    /// </summary>
    public ActitoRegion(string id, string name, string? description, string? referenceKey,
        ActitoRegionGeometry geometry, ActitoRegionAdvancedGeometry? advancedGeometry, int? major,
        double distance, string timeZone, double timeZoneOffset)
    {
        Id = id;
        Name = name;
        Description = description;
        ReferenceKey = referenceKey;
        Geometry = geometry;
        AdvancedGeometry = advancedGeometry;
        Major = major;
        Distance = distance;
        TimeZone = timeZone;
        TimeZoneOffset = timeZoneOffset;
    }
}

/// <summary>
/// Defines the basic geometry of an Actito region.
/// </summary>
public class ActitoRegionGeometry
{
    /// <summary>
    /// Geometry type.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Coordinate defining the geometry's reference point.
    /// </summary>
    public ActitoRegionCoordinate Coordinate { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoRegionGeometry"/>.
    /// </summary>
    public ActitoRegionGeometry(string type, ActitoRegionCoordinate coordinate)
    {
        Type = type;
        Coordinate = coordinate;
    }
}

/// <summary>
/// Defines an advanced geometry for complex region shapes.
/// </summary>
public class ActitoRegionAdvancedGeometry
{
    /// <summary>
    /// Geometry type.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// List of coordinates defining the geometry.
    /// </summary>
    public IList<ActitoRegionCoordinate> Coordinates { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoRegionAdvancedGeometry"/>.
    /// </summary>
    public ActitoRegionAdvancedGeometry(string type, IList<ActitoRegionCoordinate> coordinates)
    {
        Type = type;
        Coordinates = coordinates;
    }
}

/// <summary>
/// Represents a geographic coordinate.
/// </summary>
/// <remarks>
/// Coordinates are expressed in decimal degrees.
/// </remarks>
public class ActitoRegionCoordinate
{
    /// <summary>
    /// Latitude in decimal degrees.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Longitude in decimal degrees.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoRegionCoordinate"/>.
    /// </summary>
    public ActitoRegionCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
