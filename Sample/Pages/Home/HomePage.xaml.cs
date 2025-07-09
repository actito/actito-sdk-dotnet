using ActitoSdk;
using ActitoSdk.Core.Events;
using Sample.Pages.Home.Views;

namespace Sample.Pages.Home;

public partial class HomePage : ContentPage
{
    private VerticalStackLayout? _dynamicViews;

    public HomePage()
    {
        InitializeComponent();

        if (Actito.IsReady)
        {
            AddHomeCardViews();
        }

        Actito.Ready += ActitoOnReady;
        Actito.Unlaunched += ActitoOnUnlaunched;
    }

    private void AddHomeCardViews()
    {
        if (_dynamicViews != null) return;

        _dynamicViews = new VerticalStackLayout
        {
            Margin = new Thickness(0, 12),
            Spacing = 26,
            Children =
            {
                new DeviceCardView(),
                new RemoteNotificationsCardView(),
                new DoNotDisturbCardView(),
                new GeoCardView(),
                new InAppMessagingCardView(),
                new OtherFeaturesCardView()
            }
        };

        HomeCardsView.Children.Add(_dynamicViews);
    }

    private void RemoveHomeCardViews()
    {
        if (_dynamicViews == null) return;

        HomeCardsView.Children.Remove(_dynamicViews);
        _dynamicViews = null;
    }

    private void ActitoOnReady(object? sender, ActitoReadyEventArgs e)
    {
        AddHomeCardViews();
    }

    private void ActitoOnUnlaunched(object? sender, ActitoUnlaunchedEventArgs e)
    {
        RemoveHomeCardViews();
    }
}
