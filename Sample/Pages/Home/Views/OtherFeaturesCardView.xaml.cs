using Sample.Pages.AssetsGroup;
using Sample.Pages.CustomEvents;
using Sample.Pages.Scannables;

namespace Sample.Pages.Home.Views;

public partial class OtherFeaturesCardView : ContentView
{
    public OtherFeaturesCardView()
    {
        InitializeComponent();
    }

    private void NavigateToScannables(object sender, EventArgs e)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(ScannablesPage)));
    }

    private void NavigateToAssets(object sender, EventArgs e)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(AssetsGroupPage)));
    }

    private void NavigateToCustomEvents(object sender, EventArgs e)
    {
        MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(nameof(CustomEventsPage)));
    }
}
