namespace ActitoSdk.Geo.Core.Models;

public class ActitoBeacon
{
    public string Id { get; }
    public string Name { get; }
    public int Major { get; }
    public int? Minor { get; }
    public bool Triggers { get; }
    public ActitoBeaconProximity Proximity { get; }

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

public enum ActitoBeaconProximity
{
    Unknown,
    Immediate,
    Near,
    Far
}
