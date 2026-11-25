using ActitoSdk;

namespace Sample.Pages.Home.Views;

public partial class LaunchFlowCardView : ContentView
{
    public LaunchFlowCardView()
    {
        InitializeComponent();
    }

    private void ShowActitoStatusInfo(object sender, EventArgs args)
    {
        var isReady = Actito.IsReady;
        var isConfigured = Actito.IsConfigured;
        
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.DisplayAlertAsync(
                title: "Actito Status",
                message: $"Ready: {isReady}\nConfigured: {isConfigured}",
                cancel: "OK"
            );
        });
    }
}
