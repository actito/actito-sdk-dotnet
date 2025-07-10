using ActitoSdk.Inbox.Core.Models;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.Inbox.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoInboxItem FromNativeInboxItem(ActitoSdk.Inbox.iOS.Binding.ActitoInboxItem item)
    {
        return new ActitoInboxItem(
            id: item.InboxItemId,
            notification: ActitoNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeSeconds((long)item.Time.SecondsSince1970).DateTime,
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : DateTimeOffset.FromUnixTimeSeconds((long)item.Expires.SecondsSince1970).DateTime
        );
    }

    internal static ActitoSdk.Inbox.iOS.Binding.ActitoInboxItem ToNativeInboxItem(ActitoInboxItem item)
    {
        return new ActitoSdk.Inbox.iOS.Binding.ActitoInboxItem(
            inboxItemId: item.Id,
            notification: ActitoNativeConverter.ToNativeNotification(item.Notification),
            time: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(item.Time).ToUnixTimeSeconds()),
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : NSDate.FromTimeIntervalSince1970(new DateTimeOffset((DateTime)item.Expires).ToUnixTimeSeconds())
        );
    }
}
