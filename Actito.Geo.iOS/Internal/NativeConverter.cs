using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoLocation FromNativeLocation(
        ActitoSdk.Geo.iOS.Binding.ActitoLocation location
    )
    {
        return new ActitoLocation(
            latitude: location.Latitude,
            longitude: location.Longitude,
            altitude: location.Altitude,
            course: location.Course,
            speed: location.Speed,
            floor: location.Floor == null ? null : location.Floor.Int32Value,
            horizontalAccuracy: location.HorizontalAccuracy,
            verticalAccuracy: location.VerticalAccuracy,
            timestamp: DateTimeOffset.FromUnixTimeSeconds((long)location.Timestamp.SecondsSince1970).DateTime
        );
    }

    internal static ActitoRegion FromNativeRegion(
        ActitoSdk.Geo.iOS.Binding.ActitoRegion region
    )
    {
        return new ActitoRegion(
            id: region.RegionId,
            name: region.Name,
            description: region.RegionDescription,
            referenceKey: region.ReferenceKey,
            geometry: FromNativeRegionGeometry(region.Geometry),
            advancedGeometry: region.AdvancedGeometry == null
                ? null
                : FromNativeRegionAdvancedGeometry(region.AdvancedGeometry),
            major: region.Major == null ? null : region.Major.Int32Value,
            distance: region.Distance,
            timeZone: region.TimeZone,
            timeZoneOffset: region.TimeZoneOffset
        );
    }

    private static ActitoRegionGeometry FromNativeRegionGeometry(
        ActitoSdk.Geo.iOS.Binding.ActitoRegionGeometry geometry
    )
    {
        return new ActitoRegionGeometry(
            type: geometry.Type,
            coordinate: FromNativeRegionCoordinate(geometry.Coordinate)
        );
    }

    private static ActitoRegionAdvancedGeometry FromNativeRegionAdvancedGeometry(
        ActitoSdk.Geo.iOS.Binding.ActitoRegionAdvancedGeometry geometry
    )
    {
        return new ActitoRegionAdvancedGeometry(
            type: geometry.Type,
            coordinates: geometry.Coordinates.Select(FromNativeRegionCoordinate).ToList()
        );
    }

    private static ActitoRegionCoordinate FromNativeRegionCoordinate(
        ActitoSdk.Geo.iOS.Binding.ActitoRegionCoordinate coordinate
    )
    {
        return new ActitoRegionCoordinate(
            latitude: coordinate.Latitude,
            longitude: coordinate.Longitude
        );
    }

    internal static ActitoBeacon FromNativeBeacon(
        ActitoSdk.Geo.iOS.Binding.ActitoBeacon beacon
    )
    {
        return new ActitoBeacon(
            id: beacon.BeaconId,
            name: beacon.Name,
            major: beacon.Major.ToInt32(),
            minor: beacon.Minor == null ? null : beacon.Minor.Int32Value,
            triggers: beacon.Triggers,
            proximity: FromNativeBeaconProximity(beacon.Proximity)
        );
    }

    private static ActitoBeaconProximity FromNativeBeaconProximity(
        ActitoSdk.Geo.iOS.Binding.ActitoBeaconProximity proximity)
    {
        switch (proximity)
        {
            case iOS.Binding.ActitoBeaconProximity.Unknown:
                return ActitoBeaconProximity.Unknown;
            case iOS.Binding.ActitoBeaconProximity.Immediate:
                return ActitoBeaconProximity.Immediate;
            case iOS.Binding.ActitoBeaconProximity.Near:
                return ActitoBeaconProximity.Near;
            case iOS.Binding.ActitoBeaconProximity.Far:
                return ActitoBeaconProximity.Far;
            default:
                throw new ArgumentException($"Unknown beacon proximity: {proximity}");
        }
    }

    internal static ActitoVisit FromNativeVisit(
        ActitoSdk.Geo.iOS.Binding.ActitoVisit visit
    )
    {
        return new ActitoVisit(
            departureDate: DateTimeOffset.FromUnixTimeSeconds((long)visit.DepartureDate.SecondsSince1970).DateTime,
            arrivalDate: DateTimeOffset.FromUnixTimeSeconds((long)visit.ArrivalDate.SecondsSince1970).DateTime,
            latitude: visit.Latitude,
            longitude: visit.Longitude
        );
    }

    internal static ActitoHeading FromNativeHeading(
        ActitoSdk.Geo.iOS.Binding.ActitoHeading heading
    )
    {
        return new ActitoHeading(
            magneticHeading: heading.MagneticHeading,
            trueHeading: heading.TrueHeading,
            headingAccuracy: heading.HeadingAccuracy,
            x: heading.X,
            y: heading.Y,
            z: heading.Z,
            timestamp: DateTimeOffset.FromUnixTimeSeconds((long)heading.Timestamp.SecondsSince1970).DateTime
        );
    }
}
