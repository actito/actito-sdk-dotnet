using CommunityToolkit.Mvvm.ComponentModel;
using ActitoSdk.Geo;
using ActitoSdk.Geo.Core.Events;
using ActitoSdk.Geo.Core.Models;

namespace Sample.ViewModels;

public partial class BeaconsViewModel : ObservableObject
{
    [ObservableProperty] private  IList<ActitoBeacon> _beacons;

    public BeaconsViewModel()
    {
        ActitoGeo.BeaconsRanged += OnBeaconsRanged;
    }

    private void OnBeaconsRanged(object? sender, ActitoBeaconsRangedEventArgs e)
    {
        Beacons = e.Beacons;
    }
}
