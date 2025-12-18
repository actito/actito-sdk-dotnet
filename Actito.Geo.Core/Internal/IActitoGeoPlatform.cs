using ActitoSdk.Geo.Core.Events;
using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo.Core.Internal;

public interface IActitoGeoPlatform
{
    void Initialize();

    event EventHandler<ActitoLocationUpdatedEventArgs> LocationUpdated;

    event EventHandler<ActitoRegionEnteredEventArgs> RegionEntered;

    event EventHandler<ActitoRegionExitedEventArgs> RegionExited;

    event EventHandler<ActitoBeaconEnteredEventArgs> BeaconEntered;

    event EventHandler<ActitoBeaconExitedEventArgs> BeaconExited;

    event EventHandler<ActitoBeaconsRangedEventArgs> BeaconsRanged;

    event EventHandler<ActitoVisitEventArgs> Visit;

    event EventHandler<ActitoHeadingUpdatedEventArgs> HeadingUpdated;

    bool HasLocationServicesEnabled { get; }

    bool HasBluetoothEnabled { get; }

    IList<ActitoRegion> MonitoredRegions { get; }

    IList<ActitoRegion> EnteredRegions { get; }

    void EnableLocationUpdates();

    void DisableLocationUpdates();
}
