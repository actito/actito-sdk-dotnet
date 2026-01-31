using Android.Content;
using ActitoSdk.Android.Internal;
using ActitoSdk.Core.Events;
using ActitoSdk.Core.Internal;
using ActitoSdk.Core.Models;
using NativeActito = ActitoSdk.Android.Binding.Actito;

namespace ActitoSdk.Android;

public class ActitoPlatformAndroid : IActitoPlatform
{
    public void Initialize()
    {
        ActitoDotNetIntentReceiver.Platform = this;
        NativeActito.IntentReceiver = Java.Lang.Class.FromType(typeof(ActitoDotNetIntentReceiver));
    }

    public event EventHandler<ActitoReadyEventArgs>? Ready;
    public event EventHandler<ActitoUnlaunchedEventArgs>? Unlaunched;
    public event EventHandler<ActitoDeviceRegisteredEventArgs>? DeviceRegistered;

    public bool IsConfigured => NativeActito.IsConfigured;

    public bool IsReady => NativeActito.IsReady;

    public ActitoApplication? Application
    {
        get
        {
            var application = NativeActito.Application;
            return application == null ? null : ActitoNativeConverter.FromNativeApplication(application);
        }
    }

    public void Configure(Context context)
    {
        NativeActito.Configure(context);
    }

    public void Configure(Context context, string applicationKey, string applicationSecret)
    {
        NativeActito.Configure(context, applicationKey, applicationSecret);
    }
    
    public async Task LaunchAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Launch(callback);

        await callback.Task;
    }

    public async Task UnlaunchAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Unlaunch(callback);

        await callback.Task;
    }

    public async Task<ActitoApplication> FetchApplicationAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.FetchApplication(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var application = (Binding.Models.ActitoApplication)result;

        return ActitoNativeConverter.FromNativeApplication(application);
    }

    public async Task<ActitoNotification> FetchNotificationAsync(string id)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.FetchNotification(id, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (Binding.Models.ActitoNotification)result;

        return ActitoNativeConverter.FromNativeNotification(notification);
    }

    public async Task<ActitoDynamicLink> FetchDynamicLinkAsync(string url)
    {
        var uri = global::Android.Net.Uri.Parse(url);
        if (uri == null) throw new ArgumentException("Invalid url.");

        var callback = new ActitoAwaitableCallback();
        NativeActito.FetchDynamicLink(uri, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var link = (Binding.Models.ActitoDynamicLink)result;

        return ActitoNativeConverter.FromNativeDynamicLink(link);
    }

    public async Task<bool> CanEvaluateDeferredLinkAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.CanEvaluateDeferredLink(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var canEvaluateDeferredLink = (Java.Lang.Boolean)result;

        return canEvaluateDeferredLink.BooleanValue();
    }

    public async Task<bool> EvaluateDeferredLinkAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.EvaluateDeferredLink(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var evaluatedDeferredLink = (Java.Lang.Boolean)result;

        return evaluatedDeferredLink.BooleanValue();
    }

    public bool HandleTestDeviceIntent(Intent intent)
    {
        return NativeActito.HandleTestDeviceIntent(intent);
    }

    public bool HandleDynamicLinkIntent(Activity activity, Intent intent)
    {
        return NativeActito.HandleDynamicLinkIntent(activity, intent);
    }

    #region Device Module

    public ActitoDevice? CurrentDevice
    {
        get
        {
            var device = NativeActito.Device().CurrentDevice;
            return device == null ? null : ActitoNativeConverter.FromNativeDevice(device);
        }
    }

    public string? PreferredLanguage => NativeActito.Device().PreferredLanguage;

    public async Task UpdatePreferredLanguageAsync(string? language)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().UpdatePreferredLanguage(language, callback);

        await callback.Task;
    }

    public async Task UpdateUserAsync(string? userId, string? userName)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().UpdateUser(userId, userName, callback);

        await callback.Task;
    }

    public async Task<IList<string>> FetchTagsAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().FetchTags(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var tags = (System.Collections.IList)result;

        return tags
            .Cast<string>()
            .ToList();
    }

    public async Task AddTagAsync(string tag)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().AddTag(tag, callback);

        await callback.Task;
    }

    public async Task AddTagsAsync(IList<string> tags)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().AddTags(tags, callback);

        await callback.Task;
    }

    public async Task RemoveTagAsync(string tag)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().RemoveTag(tag, callback);

        await callback.Task;
    }

    public async Task RemoveTagsAsync(IList<string> tags)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().RemoveTags(tags, callback);

        await callback.Task;
    }

    public async Task ClearTagsAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().ClearTags(callback);

        await callback.Task;
    }

    public async Task<ActitoDoNotDisturb?> FetchDoNotDisturbAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().FetchDoNotDisturb(callback);

        var result = await callback.Task;
        var dnd = (Binding.Models.ActitoDoNotDisturb?)result;

        return dnd == null ? null : ActitoNativeConverter.FromNativeDoNotDisturb(dnd);
    }

    public async Task UpdateDoNotDisturbAsync(ActitoDoNotDisturb dnd)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().UpdateDoNotDisturb(ActitoNativeConverter.ToNativeDoNotDisturb(dnd), callback);

        await callback.Task;
    }

    public async Task ClearDoNotDisturbAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().ClearDoNotDisturb(callback);

        await callback.Task;
    }

    public async Task<IDictionary<string, string>> FetchUserDataAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().FetchUserData(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");

        if (result is Java.Util.IMap userData)
        {
            return userData.KeySet()
                .Cast<string>()
                .ToDictionary(
                    key => key.ToString(),
                    key => userData.Get(key)!.ToString()
                );
        }

        return new Dictionary<string, string>();
    }

    public async Task UpdateUserDataAsync(IDictionary<string, string?> userData)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Device().UpdateUserData(userData, callback);

        await callback.Task;
    }

    #endregion

    #region Events Module

    public async Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Events().LogCustom(eventName, data == null ? null : ActitoNativeConverter.ToNativeExtraDictionary(data), callback);

        await callback.Task;
    }

    #endregion


    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class ActitoDotNetIntentReceiver : Binding.ActitoIntentReceiver
    {
        internal static ActitoPlatformAndroid? Platform;

        protected override void OnReady(Context context, Binding.Models.ActitoApplication application)
        {
            Platform?.Ready?.Invoke(
                this,
                new ActitoReadyEventArgs(
                    ActitoNativeConverter.FromNativeApplication(application)
                )
            );
        }

        protected override void OnUnlaunched(Context context)
        {
            Platform?.Unlaunched?.Invoke(
                this,
                new ActitoUnlaunchedEventArgs()
            );
        }

        protected override void OnDeviceRegistered(Context context, Binding.Models.ActitoDevice device)
        {
            Platform?.DeviceRegistered?.Invoke(
                this,
                new ActitoDeviceRegisteredEventArgs(
                    ActitoNativeConverter.FromNativeDevice(device)
                )
            );
        }
    }
}
