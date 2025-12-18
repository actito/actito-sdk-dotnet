using Sample.Pages.Device;

namespace Sample.Pages.Home.Views;

public partial class DeviceCardView : ContentView
{
    public DeviceCardView()
    {
        InitializeComponent();
    }

    private void NavigateToDevice(object sender, EventArgs args)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(DevicePage)));
    }
}
