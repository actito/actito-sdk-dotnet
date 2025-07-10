using ActitoSdk.Android.Internal;
using ActitoSdk.UserInbox.Core.Models;

namespace ActitoSdk.UserInbox.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoUserInboxResponse FromNativeResponse(Binding.Models.ActitoUserInboxResponse response)
    {
        return new ActitoUserInboxResponse(
            count: response.Count,
            unread: response.Unread,
            items: response.Items.Select(FromNativeInboxItem).ToList()
        );
    }

    private static ActitoUserInboxItem FromNativeInboxItem(Binding.Models.ActitoUserInboxItem item)
    {
        return new ActitoUserInboxItem(
            id: item.Id,
            notification: ActitoNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeMilliseconds(item.Time.Time).DateTime,
            opened: item.Opened,
            expires: item.Expires == null ? null : DateTimeOffset.FromUnixTimeMilliseconds(item.Expires.Time).DateTime
        );
    }

    internal static Binding.Models.ActitoUserInboxItem ToNativeInboxItem(ActitoUserInboxItem item)
    {
        return new Binding.Models.ActitoUserInboxItem(
            id: item.Id,
            notification: ActitoNativeConverter.ToNativeNotification(item.Notification),
            time: new Java.Util.Date(new DateTimeOffset(item.Time).ToUnixTimeMilliseconds()),
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : new Java.Util.Date(new DateTimeOffset((DateTime)item.Expires).ToUnixTimeMilliseconds())
        );
    }
}
