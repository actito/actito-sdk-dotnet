namespace ActitoSdk.Geo.Core.Models;

/// <summary>
/// Represents a beacon configured in Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoBeacon"/> describes a proximity beacon that can be used to trigger
/// proximity-based events.
/// </remarks>
public class ActitoBeacon
{
    /// <summary>
    /// Unique identifier of the beacon.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Human-readable name of the beacon.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Major value of the beacon.
    /// </summary>
    /// <remarks>
    /// This value is used to group related beacons.
    /// </remarks>
    public int Major { get; }

    /// <summary>
    /// Optional minor value of the beacon.
    /// </summary>
    /// <remarks>
    /// When provided, this value identifies a specific beacon within a group.
    /// </remarks>
    public int? Minor { get; }

    /// <summary>
    /// Indicates whether this beacon can be used in triggers.
    /// </summary>
    public bool Triggers { get; }

    /// <summary>
    /// Proximity level associated with the beacon.
    /// </summary>
    public ActitoBeaconProximity Proximity { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoBeacon"/>.
    /// </summary>
    public ActitoBeacon(string id, string name, int major, int? minor, bool triggers,
        ActitoBeaconProximity proximity)
    {
        Id = id;
        Name = name;
        Major = major;
        Minor = minor;
        Triggers = triggers;
        Proximity = proximity;
    }
}

/// <summary>
/// Represents the relative distance of a beacon or region from the device.
/// </summary>
/// <remarks>
/// Used in geofencing and proximity-based notifications to indicate
/// how close a device is to a beacon.
/// </remarks>
public enum ActitoBeaconProximity
{
    /// <summary>
    /// The proximity of the beacon cannot be determined.
    /// </summary>
    Unknown,

    /// <summary>
    /// The beacon is very close to the device.
    /// </summary>
    Immediate,

    /// <summary>
    /// The beacon is nearby.
    /// </summary>
    Near,

    /// <summary>
    /// The beacon is far from the device.
    /// </summary>
    Far
}
