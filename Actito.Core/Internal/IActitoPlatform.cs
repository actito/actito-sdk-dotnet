using ActitoSdk.Core.Events;
using ActitoSdk.Core.Models;

namespace ActitoSdk.Core.Internal;

public interface IActitoPlatform
{
    void Initialize();

    event EventHandler<ActitoReadyEventArgs> Ready;
    event EventHandler<ActitoUnlaunchedEventArgs> Unlaunched;
    event EventHandler<ActitoDeviceRegisteredEventArgs> DeviceRegistered;


    bool IsConfigured { get; }

    bool IsReady { get; }

    ActitoApplication? Application { get; }

#if ANDROID
    void Configure(Android.Content.Context context);
    
    void Configure(Android.Content.Context context, string applicationKey, string applicationSecret);
#elif IOS
    void Configure();

    void Configure(string applicationKey, string applicationSecret);
#endif

    Task LaunchAsync();

    Task UnlaunchAsync();

    Task<ActitoApplication> FetchApplicationAsync();

    Task<ActitoNotification> FetchNotificationAsync(string id);

    Task<ActitoDynamicLink> FetchDynamicLinkAsync(string url);

    Task<bool> CanEvaluateDeferredLinkAsync();

    Task<bool> EvaluateDeferredLinkAsync();
    
#if ANDROID
    bool HandleTestDeviceIntent(global::Android.Content.Intent intent);
    
    bool HandleDynamicLinkIntent(global::Android.App.Activity activity, global::Android.Content.Intent intent);
#elif IOS
    bool HandleTestDeviceUrl(global::Foundation.NSUrl url);
    
    bool HandleDynamicLinkUrl(global::Foundation.NSUrl url);
#endif

    #region Device Module

    ActitoDevice? CurrentDevice { get; }

    string? PreferredLanguage { get; }

    Task UpdatePreferredLanguageAsync(string? language);

    Task UpdateUserAsync(string? userId, string? userName);

    Task<IList<string>> FetchTagsAsync();

    Task AddTagAsync(string tag);

    Task AddTagsAsync(IList<string> tags);

    Task RemoveTagAsync(string tag);

    Task RemoveTagsAsync(IList<string> tags);

    Task ClearTagsAsync();

    Task<ActitoDoNotDisturb?> FetchDoNotDisturbAsync();

    Task UpdateDoNotDisturbAsync(ActitoDoNotDisturb dnd);

    Task ClearDoNotDisturbAsync();

    Task<IDictionary<string, string>> FetchUserDataAsync();

    Task UpdateUserDataAsync(IDictionary<string, string?> userData);

    #endregion

    #region Events Module

    Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null);

    #endregion
}
