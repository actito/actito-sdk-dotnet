using CommunityToolkit.Mvvm.ComponentModel;
using ActitoSdk.Geo;

namespace Sample.ViewModels;

public partial class GeoViewModel : ObservableObject
{
    [ObservableProperty] private bool _hasLocationEnabled;

    public GeoViewModel()
    {
        UpdateLocationUpdatesStatus();
    }

    private void UpdateLocationUpdatesStatus()
    {
        HasLocationEnabled = ActitoGeo.HasLocationServicesEnabled;
    }
}
