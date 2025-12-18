using ActitoSdk.Geo.Core.Events;
using ActitoSdk.Geo.Core.Internal;
using ActitoSdk.Geo.Core.Models;
using ActitoSdk.Geo.iOS.Internal;

namespace ActitoSdk.Geo.iOS;

public class ActitoGeoPlatformIos : IActitoGeoPlatform
{
    private InternalActitoGeoDelegate? _delegate;
    private Binding.ActitoGeoNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoGeoDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoLocationUpdatedEventArgs>? LocationUpdated;
    public event EventHandler<ActitoRegionEnteredEventArgs>? RegionEntered;
    public event EventHandler<ActitoRegionExitedEventArgs>? RegionExited;
    public event EventHandler<ActitoBeaconEnteredEventArgs>? BeaconEntered;
    public event EventHandler<ActitoBeaconExitedEventArgs>? BeaconExited;
    public event EventHandler<ActitoBeaconsRangedEventArgs>? BeaconsRanged;
    public event EventHandler<ActitoVisitEventArgs>? Visit;
    public event EventHandler<ActitoHeadingUpdatedEventArgs>? HeadingUpdated;

    public bool HasLocationServicesEnabled => _native.HasLocationServicesEnabled;

    public bool HasBluetoothEnabled => _native.HasBluetoothEnabled;

    public IList<ActitoRegion> MonitoredRegions =>
        _native.MonitoredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public IList<ActitoRegion> EnteredRegions =>
        _native.EnteredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public void EnableLocationUpdates()
    {
        _native.EnableLocationUpdates();
    }

    public void DisableLocationUpdates()
    {
        _native.DisableLocationUpdates();
    }


    private sealed class InternalActitoGeoDelegate : Binding.ActitoGeoNativeBindingDelegate
    {
        private readonly ActitoGeoPlatformIos _platform;

        internal InternalActitoGeoDelegate(ActitoGeoPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidUpdateLocations(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoLocation[] locations
        )
        {
            if (locations.Length == 0) return;

            _platform.LocationUpdated?.Invoke(
                _platform,
                new ActitoLocationUpdatedEventArgs(
                    NativeConverter.FromNativeLocation(locations[0])
                )
            );
        }

        public override void DidFailWith(
            Binding.ActitoGeoNativeBinding actitoGeo,
            NSError error
        )
        {
        }

        public override void DidStartMonitoringForRegion(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoRegion region
        )
        {
        }

        public override void DidStartMonitoringForBeacon(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoBeacon beacon
        )
        {
        }

        public override void MonitoringDidFailForRegion(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoRegion region,
            NSError error
        )
        {
        }

        public override void MonitoringDidFailForBeacon(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoBeacon beacon,
            NSError error
        )
        {
        }

        public override void DidDetermineState(
            Binding.ActitoGeoNativeBinding actitoGeo,
            CoreLocation.CLRegionState state,
            Binding.ActitoRegion region
        )
        {
        }

        public override void DidDetermineState(
            Binding.ActitoGeoNativeBinding actitoGeo,
            CoreLocation.CLRegionState state,
            Binding.ActitoBeacon beacon
        )
        {
        }

        public override void DidEnterRegion(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoRegion region
        )
        {
            _platform.RegionEntered?.Invoke(
                _platform,
                new ActitoRegionEnteredEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        public override void DidEnterBeacon(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoBeacon beacon
        )
        {
            _platform.BeaconEntered?.Invoke(
                _platform,
                new ActitoBeaconEnteredEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        public override void DidExitRegion(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoRegion region
        )
        {
            _platform.RegionExited?.Invoke(
                _platform,
                new ActitoRegionExitedEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        public override void DidExitBeacon(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoBeacon beacon
        )
        {
            _platform.BeaconExited?.Invoke(
                _platform,
                new ActitoBeaconExitedEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        public override void DidVisit(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoVisit visit
        )
        {
            _platform.Visit?.Invoke(
                _platform,
                new ActitoVisitEventArgs(
                    NativeConverter.FromNativeVisit(visit)
                )
            );
        }

        public override void DidUpdateHeading(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoHeading heading
        )
        {
            _platform.HeadingUpdated?.Invoke(
                _platform,
                new ActitoHeadingUpdatedEventArgs(
                    NativeConverter.FromNativeHeading(heading)
                )
            );
        }

        public override void DidRange(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoBeacon[] beacons,
            Binding.ActitoRegion region
        )
        {
            _platform.BeaconsRanged?.Invoke(
                _platform,
                new ActitoBeaconsRangedEventArgs(
                    NativeConverter.FromNativeRegion(region),
                    beacons.Select(NativeConverter.FromNativeBeacon).ToList()
                )
            );
        }

        public override void DidFailRangingFor(
            Binding.ActitoGeoNativeBinding actitoGeo,
            Binding.ActitoRegion region,
            NSError error
        )
        {
        }
    }
}
