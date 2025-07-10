using ActitoSdk.Geo;
using Sample.Pages.Beacons;

namespace Sample.Pages.Home.Views;

public partial class GeoCardView : ContentView
{
    public GeoCardView()
    {
        InitializeComponent();
    }

    private void ShowGeoStatusInfo(object sender, EventArgs args)
    {
        var hasLocationServicesEnabled = ActitoGeo.HasLocationServicesEnabled;
        var hasBluetoothEnabled = ActitoGeo.HasBluetoothEnabled;

        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.DisplayAlert(
                title: "Location Status",
                message: $"\nhasLocationServicesEnabled: {hasLocationServicesEnabled}" +
                         $"\nhasBluetoothEnabled: {hasBluetoothEnabled}",
                cancel: "OK"
            );
        });
    }

    private void NavigateToBeacons(object sender, EventArgs args)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(BeaconsPage)));
    }

    private void OnLocationSwitchToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (!await EnsureForegroundLocationPermission())
                {
                    LocationSwitch.IsCodeToggled = false;
                    return;
                }

                if (await EnsureBackgroundLocationPermission())
                {
                    await EnsureBluetoothPermission();
                }

                ActitoGeo.EnableLocationUpdates();
            });
        }
        else
        {
            ActitoGeo.DisableLocationUpdates();
        }
    }

    private async Task<bool> EnsureForegroundLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted) return true;

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            await Shell.Current.DisplayAlert(
                title: "Location Rational",
                message: "Location Foreground rational message.",
                cancel: "OK"
            );
        }

        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        return status == PermissionStatus.Granted;
    }

    private async Task<bool> EnsureBackgroundLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        if (status == PermissionStatus.Granted) return true;

        if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
        {
            await Shell.Current.DisplayAlert(
                title: "Location Rational",
                message: "Location Background rational message.",
                cancel: "OK"
            );
        }

        status = await Permissions.RequestAsync<Permissions.LocationAlways>();

        return status == PermissionStatus.Granted;
    }

    private async Task<bool> EnsureBluetoothPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
        if (status == PermissionStatus.Granted) return true;

        if (Permissions.ShouldShowRationale<Permissions.Bluetooth>())
        {
            await Shell.Current.DisplayAlert(
                title: "Bluetooth Rational",
                message: "Bluetooth rational message.",
                cancel: "OK"
            );
        }

        status = await Permissions.RequestAsync<Permissions.Bluetooth>();

        return status == PermissionStatus.Granted;
    }
}
