using Sample.Pages.AssetsGroup;
using Sample.Pages.CustomEvents;

namespace Sample.Pages.Home.Views;

public partial class OtherFeaturesCardView : ContentView
{
    public OtherFeaturesCardView()
    {
        InitializeComponent();
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
