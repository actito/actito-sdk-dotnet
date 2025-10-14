using Android.Content;
using ActitoSdk.Geo.Android.Internal;
using ActitoSdk.Geo.Core.Events;
using ActitoSdk.Geo.Core.Internal;
using ActitoSdk.Geo.Core.Models;
using NativeActito = ActitoSdk.Geo.Android.Binding.ActitoGeo;

namespace ActitoSdk.Geo.Android;

public class ActitoGeoPlatformAndroid : IActitoGeoPlatform
{
    public void Initialize()
    {
        ActitoDotNetGeoIntentReceiver.Platform = this;
        NativeActito.IntentReceiver = Java.Lang.Class.FromType(typeof(ActitoDotNetGeoIntentReceiver));
    }

    public event EventHandler<ActitoLocationUpdatedEventArgs>? LocationUpdated;
    public event EventHandler<ActitoRegionEnteredEventArgs>? RegionEntered;
    public event EventHandler<ActitoRegionExitedEventArgs>? RegionExited;
    public event EventHandler<ActitoBeaconEnteredEventArgs>? BeaconEntered;
    public event EventHandler<ActitoBeaconExitedEventArgs>? BeaconExited;
    public event EventHandler<ActitoBeaconsRangedEventArgs>? BeaconsRanged;
    public event EventHandler<ActitoVisitEventArgs>? Visit;
    public event EventHandler<ActitoHeadingUpdatedEventArgs>? HeadingUpdated;

    public bool HasLocationServicesEnabled => NativeActito.HasLocationServicesEnabled;

    public bool HasBluetoothEnabled => NativeActito.HasBluetoothEnabled;

    public IList<ActitoRegion> MonitoredRegions =>
        NativeActito.MonitoredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public IList<ActitoRegion> EnteredRegions =>
        NativeActito.EnteredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public void EnableLocationUpdates()
    {
        NativeActito.EnableLocationUpdates();
    }

    public void DisableLocationUpdates()
    {
        NativeActito.DisableLocationUpdates();
    }


    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class ActitoDotNetGeoIntentReceiver : Binding.ActitoGeoIntentReceiver
    {
        internal static ActitoGeoPlatformAndroid? Platform;

        protected override void OnLocationUpdated(Context context, Binding.Models.ActitoLocation location)
        {
            Platform?.LocationUpdated?.Invoke(
                this,
                new ActitoLocationUpdatedEventArgs(
                    NativeConverter.FromNativeLocation(location)
                )
            );
        }

        protected override void OnRegionEntered(Context context, Binding.Models.ActitoRegion region)
        {
            Platform?.RegionEntered?.Invoke(
                this,
                new ActitoRegionEnteredEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        protected override void OnRegionExited(Context context, Binding.Models.ActitoRegion region)
        {
            Platform?.RegionExited?.Invoke(
                this,
                new ActitoRegionExitedEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        protected override void OnBeaconEntered(Context context, Binding.Models.ActitoBeacon beacon)
        {
            Platform?.BeaconEntered?.Invoke(
                this,
                new ActitoBeaconEnteredEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        protected override void OnBeaconExited(Context context, Binding.Models.ActitoBeacon beacon)
        {
            Platform?.BeaconExited?.Invoke(
                this,
                new ActitoBeaconExitedEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        protected override void OnBeaconsRanged(
            Context context,
            Binding.Models.ActitoRegion region,
            IList<Binding.Models.ActitoBeacon> beacons
        )
        {
            Platform?.BeaconsRanged?.Invoke(
                this,
                new ActitoBeaconsRangedEventArgs(
                    NativeConverter.FromNativeRegion(region),
                    beacons.Select(NativeConverter.FromNativeBeacon).ToList()
                )
            );
        }
    }
}
