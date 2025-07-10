using ActitoSdk.Android.Internal;
using ActitoSdk.UserInbox.Core.Internal;
using ActitoSdk.UserInbox.Core.Models;
using ActitoSdk.Core.Models;
using ActitoSdk.UserInbox.Android.Internal;
using NativeActito = ActitoSdk.UserInbox.Android.Binding.ActitoUserInboxCompat;

namespace ActitoSdk.UserInbox.Android;

public class ActitoUserInboxPlatformAndroid : IActitoUserInboxPlatform
{
    public void Initialize()
    {
    }

    public Task<ActitoUserInboxResponse> ParseResponseAsync(string json)
    {
        var response = NativeActito.ParseResponse(json);
        return Task.FromResult(NativeConverter.FromNativeResponse(response));
    }

    public async Task<ActitoUserInboxResponse> ParseResponseAsync(HttpResponseMessage response)
    {
        var responseStr = await response.Content.ReadAsStringAsync();
        return await ParseResponseAsync(responseStr);
    }

    public async Task<ActitoNotification> OpenAsync(ActitoUserInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Open(NativeConverter.ToNativeInboxItem(item), callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (ActitoSdk.Android.Binding.Models.ActitoNotification)result;

        return ActitoNativeConverter.FromNativeNotification(notification);
    }

    public async Task MarkAsReadAsync(ActitoUserInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.MarkAsRead(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task RemoveAsync(ActitoUserInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Remove(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }
}
