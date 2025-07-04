using ActitoSdk.Geo.Core.Events;
using ActitoSdk.Geo.Core.Internal;
using ActitoSdk.Geo.Core.Models;

namespace ActitoSdk.Geo;

public static class ActitoGeo
{
    private static readonly Lazy<IActitoGeoPlatform> Implementation = new(() =>
    {
        var instance = CreateActito();
        instance.Initialize();

        return instance;
    });

    private static IActitoGeoPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    /// <summary>
    /// Called when a new location update is received.
    /// </summary>
    public static event EventHandler<ActitoLocationUpdatedEventArgs> LocationUpdated
    {
        add => Platform.LocationUpdated += value;
        remove => Platform.LocationUpdated -= value;
    }

    /// <summary>
    /// Called when the user enters a monitored region.
    /// </summary>
    public static event EventHandler<ActitoRegionEnteredEventArgs> RegionEntered
    {
        add => Platform.RegionEntered += value;
        remove => Platform.RegionEntered -= value;
    }

    /// <summary>
    /// Called when the user exits a monitored region.
    /// </summary>
    public static event EventHandler<ActitoRegionExitedEventArgs> RegionExited
    {
        add => Platform.RegionExited += value;
        remove => Platform.RegionExited -= value;
    }

    /// <summary>
    /// Called when the user enters the proximity of a beacon.
    /// </summary>
    public static event EventHandler<ActitoBeaconEnteredEventArgs> BeaconEntered
    {
        add => Platform.BeaconEntered += value;
        remove => Platform.BeaconEntered -= value;
    }

    /// <summary>
    /// Called when the user exits the proximity of a beacon.
    /// </summary>
    public static event EventHandler<ActitoBeaconExitedEventArgs> BeaconExited
    {
        add => Platform.BeaconExited += value;
        remove => Platform.BeaconExited -= value;
    }

    /// <summary>
    /// Called when beacons are ranged in a monitored region.
    ///
    /// This method provides the list of beacons currently detected within the given region.
    /// </summary>
    public static event EventHandler<ActitoBeaconsRangedEventArgs> BeaconsRanged
    {
        add => Platform.BeaconsRanged += value;
        remove => Platform.BeaconsRanged -= value;
    }

    /// <summary>
    /// Called when the device registers a location visit.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    public static event EventHandler<ActitoVisitEventArgs> Visit
    {
        add => Platform.Visit += value;
        remove => Platform.Visit -= value;
    }

    /// <summary>
    /// Called when there is an update to the device’s heading.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    public static event EventHandler<ActitoHeadingUpdatedEventArgs> HeadingUpdated
    {
        add => Platform.HeadingUpdated += value;
        remove => Platform.HeadingUpdated -= value;
    }


    /// <summary>
    /// Indicates whether location services are enabled.
    /// </summary>
    public static bool HasLocationServicesEnabled => Platform.HasLocationServicesEnabled;

    /// <summary>
    /// Indicates whether Bluetooth is enabled.
    /// </summary>
    public static bool HasBluetoothEnabled => Platform.HasBluetoothEnabled;

    /// <summary>
    /// Provides a list of regions currently being monitored.
    /// </summary>
    public static IList<ActitoRegion> MonitoredRegions => Platform.MonitoredRegions;

    /// <summary>
    /// Provides a list of regions the user has entered.
    /// </summary>
    public static IList<ActitoRegion> EnteredRegions => Platform.EnteredRegions;

    /// <summary>
    /// Enables location updates, activating location tracking, region monitoring, and beacon detection.
    ///
    /// <strong>Note</strong>: This function requires explicit location permissions from the user.
    /// Starting with Android 10 (API level 29), background location access requires the ACCESS_BACKGROUND_LOCATION
    /// permission. For beacon detection, Bluetooth permissions are also necessary.
    /// Ensure all permissions are requested before invoking this method.
    ///
    /// The behavior varies based on granted permissions:
    /// - <strong>Permission denied</strong>: Clears the device's location information.
    /// - <strong>When In Use permission granted</strong>: Tracks location only while the app is in use.
    /// - <strong>Always permission granted</strong> Enables geofencing capabilities.
    /// - <strong>Always + Bluetooth permissions granted</strong> Enables geofencing and beacon detection.
    /// </summary>
    public static void EnableLocationUpdates() => Platform.EnableLocationUpdates();

    /// <summary>
    /// Disables location updates.
    ///
    /// This method stops receiving location updates, monitoring regions, and detecting nearby beacons.
    /// </summary>
    public static void DisableLocationUpdates() => Platform.DisableLocationUpdates();


    private static IActitoGeoPlatform CreateActito()
    {
#if ANDROID
        return new Android.ActitoGeoPlatformAndroid();
#elif IOS
        return new iOS.ActitoGeoPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Actito.");
    }
}
