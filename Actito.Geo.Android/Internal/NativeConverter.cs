using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoLocation FromNativeLocation(
        Binding.Models.ActitoLocation location
    )
    {
        return new ActitoLocation(
            latitude: location.Latitude,
            longitude: location.Longitude,
            altitude: location.Altitude,
            course: location.Course,
            speed: location.Speed,
            floor: null,
            horizontalAccuracy: location.HorizontalAccuracy,
            verticalAccuracy: location.VerticalAccuracy,
            timestamp: DateTimeOffset.FromUnixTimeMilliseconds(location.Timestamp.Time).DateTime
        );
    }

    internal static ActitoRegion FromNativeRegion(
        Binding.Models.ActitoRegion region
    )
    {
        return new ActitoRegion(
            id: region.Id,
            name: region.Name,
            description: region.Description,
            referenceKey: region.ReferenceKey,
            geometry: FromNativeRegionGeometry(region.GetGeometry()),
            advancedGeometry: region.GetAdvancedGeometry() == null
                ? null
                : FromNativeRegionAdvancedGeometry(region.GetAdvancedGeometry()!),
            major: region.Major?.IntValue(),
            distance: region.Distance,
            timeZone: region.TimeZone,
            timeZoneOffset: region.TimeZoneOffset
        );
    }

    private static ActitoRegionGeometry FromNativeRegionGeometry(
        Binding.Models.ActitoRegion.Geometry geometry
    )
    {
        return new ActitoRegionGeometry(
            type: geometry.Type,
            coordinate: FromNativeRegionCoordinate(geometry.Coordinate)
        );
    }

    private static ActitoRegionAdvancedGeometry FromNativeRegionAdvancedGeometry(
        Binding.Models.ActitoRegion.AdvancedGeometry geometry
    )
    {
        return new ActitoRegionAdvancedGeometry(
            type: geometry.Type,
            coordinates: geometry.Coordinates.Select(FromNativeRegionCoordinate).ToList()
        );
    }

    private static ActitoRegionCoordinate FromNativeRegionCoordinate(
        Binding.Models.ActitoRegion.Coordinate coordinate
    )
    {
        return new ActitoRegionCoordinate(
            latitude: coordinate.Latitude,
            longitude: coordinate.Longitude
        );
    }

    internal static ActitoBeacon FromNativeBeacon(
        Binding.Models.ActitoBeacon beacon
    )
    {
        return new ActitoBeacon(
            id: beacon.Id,
            name: beacon.Name,
            major: beacon.Major,
            minor: beacon.Minor?.IntValue(),
            triggers: beacon.Triggers,
            proximity: FromNativeBeaconProximity(beacon.GetProximity())
        );
    }

    internal static ActitoBeaconProximity FromNativeBeaconProximity(
        Binding.Models.ActitoBeacon.Proximity proximity
    )
    {
        if (proximity == Binding.Models.ActitoBeacon.Proximity.Unknown)
            return ActitoBeaconProximity.Unknown;

        if (proximity == Binding.Models.ActitoBeacon.Proximity.Immediate)
            return ActitoBeaconProximity.Immediate;

        if (proximity == Binding.Models.ActitoBeacon.Proximity.Near)
            return ActitoBeaconProximity.Near;

        if (proximity == Binding.Models.ActitoBeacon.Proximity.Far)
            return ActitoBeaconProximity.Far;

        throw new ArgumentException($"Unknown beacon proximity: {proximity}");
    }
}
