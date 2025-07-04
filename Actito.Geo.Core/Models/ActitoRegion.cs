namespace ActitoSdk.Geo.Core.Models;

public class ActitoRegion
{
    public string Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public string? ReferenceKey { get; }
    public ActitoRegionGeometry Geometry { get; }
    public ActitoRegionAdvancedGeometry? AdvancedGeometry { get; }
    public int? Major { get; }
    public double Distance { get; }
    public string TimeZone { get; }
    public double TimeZoneOffset { get; }

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

public class ActitoRegionGeometry
{
    public string Type { get; }
    public ActitoRegionCoordinate Coordinate { get; }

    public ActitoRegionGeometry(string type, ActitoRegionCoordinate coordinate)
    {
        Type = type;
        Coordinate = coordinate;
    }
}

public class ActitoRegionAdvancedGeometry
{
    public string Type { get; }
    public IList<ActitoRegionCoordinate> Coordinates { get; }

    public ActitoRegionAdvancedGeometry(string type, IList<ActitoRegionCoordinate> coordinates)
    {
        Type = type;
        Coordinates = coordinates;
    }
}

public class ActitoRegionCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public ActitoRegionCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
