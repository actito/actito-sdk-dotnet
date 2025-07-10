using System.Windows.Input;
using ActitoSdk;

namespace Sample.UserInbox.Pages.Home.Views;

public partial class LaunchFlowCardView : ContentView
{
    public static readonly BindableProperty IsReadyProperty =
        BindableProperty.Create(nameof(IsReady), typeof(bool), typeof(LaunchFlowCardView), false);

    public static readonly BindableProperty LaunchCommandProperty =
        BindableProperty.Create(nameof(LaunchCommand), typeof(ICommand), typeof(LaunchFlowCardView));

    public static readonly BindableProperty UnlaunchCommandProperty =
        BindableProperty.Create(nameof(UnlaunchCommand), typeof(ICommand), typeof(LaunchFlowCardView));

    public LaunchFlowCardView()
    {
        InitializeComponent();
    }

    public bool IsReady
    {
        get => (bool)GetValue(IsReadyProperty);
        set => SetValue(IsReadyProperty, value);
    }

    public ICommand LaunchCommand
    {
        get => (ICommand)GetValue(LaunchCommandProperty);
        set => SetValue(LaunchCommandProperty, value);
    }

    public ICommand UnlaunchCommand
    {
        get => (ICommand)GetValue(UnlaunchCommandProperty);
        set => SetValue(UnlaunchCommandProperty, value);
    }

    private void ShowActitoStatusInfo(object sender, EventArgs args)
    {
        var isReady = Actito.IsReady;
        var isConfigured = Actito.IsConfigured;

        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.DisplayAlert(
                "Actito Status",
                $"Ready: {isReady}\nConfigured: {isConfigured}",
                "OK"
            );
        });
    }
}
