using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ActitoSdk;
using ActitoSdk.Core.Events;

namespace Sample.ViewModels;

public partial class LaunchFlowViewModel : ObservableObject
{
    [ObservableProperty] private bool _isReady;

    public LaunchFlowViewModel()
    {
        IsReady = Actito.IsReady;

        Actito.Ready += OnReady;
        Actito.Unlaunched += OnUnlaunch;
    }

    private void OnReady(object? sender, ActitoReadyEventArgs e)
    {
        IsReady = true;
    }

    private void OnUnlaunch(object? sender, ActitoUnlaunchedEventArgs e)
    {
        IsReady = false;
    }

    [RelayCommand]
    private async Task Launch()
    {
        try
        {
            await Actito.LaunchAsync();
            Console.WriteLine("Launch success.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Launch failed: {exception}");
        }
    }

    [RelayCommand]
    private async Task Unlaunch()
    {
        try
        {
            await Actito.UnlaunchAsync();
            Console.WriteLine("Unlaunch success.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unlaunch failed: {exception}");
        }
    }
}
