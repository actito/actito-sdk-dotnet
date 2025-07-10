using ActitoSdk.Android.Internal;
using ActitoSdk.Inbox.Core.Models;

namespace ActitoSdk.Inbox.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoInboxItem FromNativeInboxItem(
        ActitoSdk.Inbox.Android.Binding.Models.ActitoInboxItem item)
    {
        return new ActitoInboxItem(
            id: item.Id,
            notification: ActitoNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeMilliseconds(item.Time.Time).DateTime,
            opened: item.Opened,
            expires: item.Expires == null ? null : DateTimeOffset.FromUnixTimeMilliseconds(item.Expires.Time).DateTime
        );
    }

    internal static ActitoSdk.Inbox.Android.Binding.Models.ActitoInboxItem ToNativeInboxItem(
        ActitoInboxItem item)
    {
        return new ActitoSdk.Inbox.Android.Binding.Models.ActitoInboxItem(
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
