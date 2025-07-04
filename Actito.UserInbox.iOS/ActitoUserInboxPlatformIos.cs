using ActitoSdk.Core.Models;
using ActitoSdk.UserInbox.Core.Internal;
using ActitoSdk.UserInbox.Core.Models;
using ActitoSdk.UserInbox.iOS.Internal;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.UserInbox.iOS;

public class ActitoUserInboxPlatformIos : IActitoUserInboxPlatform
{
    private Binding.ActitoUserInboxNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<ActitoUserInboxResponse> ParseResponseAsync(string json)
    {
        TaskCompletionSource<ActitoUserInboxResponse> completion = new();

        _native.ParseResponseFromString(
            json,
            response => completion.TrySetResult(NativeConverter.FromNativeResponse(response)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public async Task<ActitoUserInboxResponse> ParseResponseAsync(HttpResponseMessage response)
    {
        var responseStr = await response.Content.ReadAsStringAsync();
        return await ParseResponseAsync(responseStr);
    }

    public Task<ActitoNotification> OpenAsync(ActitoUserInboxItem item)
    {
        TaskCompletionSource<ActitoNotification> completion = new();

        _native.Open(
            NativeConverter.ToNativeInboxItem(item),
            notification => completion.TrySetResult(ActitoNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAsReadAsync(ActitoUserInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.MarkAsRead(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveAsync(ActitoUserInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.Remove(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
