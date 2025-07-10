using ActitoSdk;
using ActitoSdk.Core.Events;
using ActitoSdk.Push;
using ActitoSdk.Push.Core.Events;
using ActitoSdk.UserInbox;
using Auth0.OidcClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duende.IdentityModel.OidcClient.Browser;
using Sample.UserInbox.Network;

namespace Sample.UserInbox.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly Auth0Client _auth0Client;
    private readonly UserInboxService _userInboxService;

    [ObservableProperty] private string? _accessToken;
    [ObservableProperty] private int _badge;
    [ObservableProperty] private bool _hasNotificationsEnabled;
    [ObservableProperty] private bool _isLoggedIn;
    [ObservableProperty] private bool _isReady;

    public HomeViewModel(Auth0Client auth0Client, UserInboxService userInboxService)
    {
        _auth0Client = auth0Client;
        _userInboxService = userInboxService;
        HasNotificationsEnabled = ActitoPush.HasRemoteNotificationsEnabled && ActitoPush.AllowedUI;
        IsReady = Actito.IsReady;

        Actito.Ready += OnReady;
        Actito.Unlaunched += OnUnlaunch;
        ActitoPush.NotificationSettingsChanged += OnNotificationsSettingsChanged;
        ActitoPush.NotificationReceived += OnNotificationReceived;
        ActitoPush.NotificationOpened += OnNotificationOpened;
    }

    #region Authentication Card

    [RelayCommand]
    private async Task Login()
    {
        var result = await _auth0Client.LoginAsync();

        if (result.IsError)
        {
            Console.WriteLine($"Login failed: {result.ErrorDescription}");
            return;
        }

        await RegisterWithUser(result.AccessToken);

        AccessToken = result.AccessToken;
        IsLoggedIn = true;
        
        RefreshBadge();
    }

    [RelayCommand]
    private async Task Logout()
    {
        var token = AccessToken;
        if (token == null)
        {
            Console.WriteLine("Can not logout without valid token.");
            return;
        }

        var result = await _auth0Client.LogoutAsync();

        if (result != BrowserResultType.Success)
        {
            Console.WriteLine($"Logout failed: {result}");
            return;
        }

        await RegisterAsAnonymous(token);

        AccessToken = null;
        IsLoggedIn = false;
    }

    #endregion

    #region Launch Flow Card

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

    #endregion

    #region Remote Notifications Card

    private void OnNotificationsSettingsChanged(object? sender, ActitoNotificationSettingsChangedEventArgs e)
    {
        HasNotificationsEnabled = ActitoPush.HasRemoteNotificationsEnabled && ActitoPush.AllowedUI;
    }

    private void OnNotificationReceived(object? sender, ActitoNotificationReceivedEventArgs e)
    {
        RefreshBadge();
    }

    private void OnNotificationOpened(object? sender, ActitoNotificationOpenedEventArgs e)
    {
        RefreshBadge();
    }

    [RelayCommand]
    private async Task EnableRemoteNotifications()
    {
        try
        {
            await ActitoPush.EnableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = false;
            Console.WriteLine($"Failed to enable remote notifications: {e.Message}");
        }
    }

    [RelayCommand]
    private async Task DisableRemoteNotifications()
    {
        try
        {
            await ActitoPush.DisableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = true;
            Console.WriteLine($"Failed to disable remote notifications: {e.Message}");
        }
    }

    #endregion

    #region Inbox Services

    private async Task RegisterWithUser(string token)
    {
        await _userInboxService.RegisterDeviceWithUser(token);
    }

    private async Task RegisterAsAnonymous(string token)
    {
        await _userInboxService.RegisterDeviceAsAnonymous(token);
    }

    internal void RefreshBadge()
    {
        var token = AccessToken;
        if (token == null)
        {
            Console.WriteLine("Could not refresh badge. Access token is required");
            return;
        }

        Task.Run(async () =>
        {
            try
            {
                var requestResponse = await _userInboxService.GetInboxResponse(token);
                var inboxResponse = await ActitoUserInbox.ParseResponseAsync(requestResponse);
                Badge = inboxResponse.Unread;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to refresh badge: {e.Message}");
            }
        });
    }

    #endregion
}
