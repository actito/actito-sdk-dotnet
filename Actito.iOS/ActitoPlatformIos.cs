using ActitoSdk.Core.Events;
using ActitoSdk.Core.Internal;
using ActitoSdk.Core.Models;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.iOS;

public class ActitoPlatformIos : IActitoPlatform
{
    private InternalActitoDelegate? _delegate;
    private Binding.ActitoNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoReadyEventArgs>? Ready;
    public event EventHandler<ActitoUnlaunchedEventArgs>? Unlaunched;
    public event EventHandler<ActitoDeviceRegisteredEventArgs>? DeviceRegistered;

    public bool IsConfigured => _native.IsConfigured;

    public bool IsReady => _native.IsReady;

    public void Configure()
    {
        _native.Configure();
    }

    public void Configure(string applicationKey, string applicationSecret)
    {
        _native.ConfigureWithApplicationKey(applicationKey, applicationSecret);
    }

    public ActitoApplication? Application
    {
        get
        {
            var application = _native.Application;
            return application == null ? null : ActitoNativeConverter.FromNativeApplication(application);
        }
    }

    public Task LaunchAsync()
    {
        TaskCompletionSource completion = new();

        _native.Launch(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UnlaunchAsync()
    {
        TaskCompletionSource completion = new();

        _native.Unlaunch(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<ActitoApplication> FetchApplicationAsync()
    {
        TaskCompletionSource<ActitoApplication> completion = new();

        _native.FetchApplication
        (
            application => completion.TrySetResult(ActitoNativeConverter.FromNativeApplication(application)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<ActitoNotification> FetchNotificationAsync(string id)
    {
        TaskCompletionSource<ActitoNotification> completion = new();

        _native.FetchNotification(
            id,
            notification => completion.TrySetResult(ActitoNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<ActitoDynamicLink> FetchDynamicLinkAsync(string url)
    {
        TaskCompletionSource<ActitoDynamicLink> completion = new();

        _native.FetchDynamicLink(
            url,
            link => completion.TrySetResult(ActitoNativeConverter.FromNativeDynamicLink(link)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<bool> CanEvaluateDeferredLinkAsync()
    {
        TaskCompletionSource<bool> completion = new();

        completion.TrySetResult(_native.CanEvaluateDeferredLink);

        return completion.Task;
    }

    public Task<bool> EvaluateDeferredLinkAsync()
    {
        TaskCompletionSource<bool> completion = new();

        _native.EvaluateDeferredLink(
            evaluated => completion.TrySetResult(evaluated),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public bool HandleTestDeviceUrl(NSUrl url)
    {
        return _native.HandleTestDeviceUrl(url);
    }

    public bool HandleDynamicLinkUrl(NSUrl url)
    {
        return _native.HandleDynamicLinkUrl(url);
    }

    #region Device Module

    public ActitoDevice? CurrentDevice
    {
        get
        {
            var device = _native.CurrentDevice;
            return device == null ? null : ActitoNativeConverter.FromNativeDevice(device);
        }
    }

    public string? PreferredLanguage => _native.PreferredLanguage?.ToString();

    public Task UpdatePreferredLanguageAsync(string? language)
    {
        TaskCompletionSource completion = new();

        _native.UpdatePreferredLanguage(
            language,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateUserAsync(string? userId, string? userName)
    {
        TaskCompletionSource completion = new();

        _native.UpdateUserWithUserId(
            userId,
            userName,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<IList<string>> FetchTagsAsync()
    {
        TaskCompletionSource<IList<string>> completion = new();

        _native.FetchTags(
            tags => completion.TrySetResult(tags.ToArray().Select(item => item.ToString()).ToList()),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task AddTagAsync(string tag)
    {
        TaskCompletionSource completion = new();

        _native.AddTag(
            tag,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task AddTagsAsync(IList<string> tags)
    {
        TaskCompletionSource completion = new();

        _native.AddTags(
            tags.ToArray(),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveTagAsync(string tag)
    {
        TaskCompletionSource completion = new();

        _native.RemoveTag(
            tag,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveTagsAsync(IList<string> tags)
    {
        TaskCompletionSource completion = new();

        _native.RemoveTags(
            tags.ToArray(),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearTagsAsync()
    {
        TaskCompletionSource completion = new();

        _native.ClearTags(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<ActitoDoNotDisturb?> FetchDoNotDisturbAsync()
    {
        TaskCompletionSource<ActitoDoNotDisturb?> completion = new();

        _native.FetchDoNotDisturb(
            dnd => completion.TrySetResult(dnd == null ? null : ActitoNativeConverter.FromNativeDoNotDisturb(dnd)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateDoNotDisturbAsync(ActitoDoNotDisturb dnd)
    {
        TaskCompletionSource completion = new();

        _native.UpdateDoNotDisturb(
            ActitoNativeConverter.ToNativeDoNotDisturb(dnd),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearDoNotDisturbAsync()
    {
        TaskCompletionSource completion = new();

        _native.ClearDoNotDisturb(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<IDictionary<string, string>> FetchUserDataAsync()
    {
        TaskCompletionSource<IDictionary<string, string>> completion = new();

        _native.FetchUserData(
            userData => completion.TrySetResult(
                userData.ToDictionary<KeyValuePair<NSString, NSString>, string, string>(
                    item => item.Key.ToString(),
                    item => item.Value.ToString()
                )
            ),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateUserDataAsync(IDictionary<string, string?> userData)
    {
        TaskCompletionSource completion = new();

        _native.UpdateUserData(
            userData.Count == 0
                ? new NSDictionary<NSString, NSObject>()
                : NSDictionary<NSString, NSObject>.FromObjectsAndKeys(
                    userData.Values.Select(value =>
                        value != null ? (NSObject)new NSString(value) : NSNull.Null).ToArray(),
                    userData.Keys.Select(key => new NSString(key)).ToArray(),
                    userData.Count
                ),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    #endregion

    #region Events Module

    public Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null)
    {
        TaskCompletionSource completion = new();

        _native.LogCustom(
            eventName,
            data == null ? null : ActitoNativeConverter.ToNativeExtraDictionary(data),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    #endregion


    private sealed class InternalActitoDelegate : Binding.ActitoNativeBindingDelegate
    {
        private readonly ActitoPlatformIos _plugin;

        internal InternalActitoDelegate(ActitoPlatformIos plugin)
        {
            _plugin = plugin;
        }

        public override void Actito(Binding.ActitoNativeBinding actito,
            Binding.ActitoApplication application)
        {
            var args = new ActitoReadyEventArgs(ActitoNativeConverter.FromNativeApplication(application));
            _plugin.Ready?.Invoke(_plugin, args);
        }

        public override void ActitoDidUnlaunch(Binding.ActitoNativeBinding actito)
        {
            var args = new ActitoUnlaunchedEventArgs();
            _plugin.Unlaunched?.Invoke(_plugin, args);
        }

        public override void Actito(Binding.ActitoNativeBinding actito, Binding.ActitoDevice device)
        {
            var args = new ActitoDeviceRegisteredEventArgs(ActitoNativeConverter.FromNativeDevice(device));
            _plugin.DeviceRegistered?.Invoke(_plugin, args);
        }
    }
}
