using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ActitoSdk;

namespace Sample.ViewModels;

public partial class DeviceViewModel : ObservableObject
{
    [ObservableProperty] private string? _userId;
    [ObservableProperty] private string? _userName;
    [ObservableProperty] private string? _userDnD;
    [ObservableProperty] private string? _userPreferredLanguage;
    [ObservableProperty] private IDictionary<string, string> _userData = new Dictionary<string, string>();

    public DeviceViewModel()
    {
        LoadDeviceData();
    }

    private void LoadDeviceData()
    {
        var currentDevice = Actito.Device.CurrentDevice;

        if (currentDevice == null)
        {
            return;
        }

        var preferredLanguage = Actito.Device.PreferredLanguage;

        UserId = currentDevice.Id;
        UserName = currentDevice.UserName ?? "NULL";
        UserPreferredLanguage = preferredLanguage ?? "NULL";
        UserDnD = currentDevice.Dnd != null ? $"{currentDevice.Dnd.Start} - {currentDevice.Dnd.End} " : "NULL";
        UserData = currentDevice.UserData;
    }

    [RelayCommand]
    private async Task UpdateUser()
    {
        try
        {
            await Actito.Device.UpdateUserAsync("Notificarista@notifica.re", "Notificarista");
            Console.WriteLine("Updated user successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to update user: {e.Message}");
        }
    }

    [RelayCommand]
    private async Task UpdateUserAsAnonymous()
    {
        try
        {
            await Actito.Device.UpdateUserAsync(null, null);
            Console.WriteLine("Updated user as anonymous successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to update user as anonymous: {e.Message}");
        }
    }

    [RelayCommand]
    private async Task UpdatePreferredLanguage()
    {
        try
        {
            await Actito.Device.UpdatePreferredLanguageAsync("nl-NL");
            Console.WriteLine("Updated preferred language successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to update preferred language: {e.Message}");
        }
    }

    [RelayCommand]
    private async Task ClearPreferredLanguage()
    {
        try
        {
            await Actito.Device.UpdatePreferredLanguageAsync(null);
            Console.WriteLine("Cleared preferred language successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to clear preferred language: {e.Message}");
        }
    }

    [RelayCommand]
    private async Task UpdateUserData()
    {
        try
        {
            await Actito.Device.UpdateUserDataAsync(new Dictionary<string, string?>
                {
                    { "firstName", "FirstNameExample" },
                    { "lastName", "LastNameExample" }
                }
            );

            Console.WriteLine("User data updated successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to update user data: {e.Message}");
        }
    }
    
    [RelayCommand]
    private async Task ResetUserData()
    {
        try
        {
            await Actito.Device.UpdateUserDataAsync(new Dictionary<string, string?>
                {
                    { "firstName", null },
                    { "lastName", "LastNameExample" }
                }
            );

            Console.WriteLine("User data reset successfully.");

            LoadDeviceData();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to reset user data: {e.Message}");
        }
    }
}
