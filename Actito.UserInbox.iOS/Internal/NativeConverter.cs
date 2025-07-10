using ActitoSdk.UserInbox.Core.Models;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.UserInbox.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoUserInboxResponse FromNativeResponse(Binding.ActitoUserInboxResponse response)
    {
        return new ActitoUserInboxResponse(
            count: response.Count.ToInt32(),
            unread: response.Unread.ToInt32(),
            items: response.Items.Select(FromNativeInboxItem).ToList()
        );
    }
    
    private static ActitoUserInboxItem FromNativeInboxItem(Binding.ActitoUserInboxItem item)
    {
        return new ActitoUserInboxItem(
            id: item.InboxItemId,
            notification: ActitoNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeSeconds((long)item.Time.SecondsSince1970).DateTime,
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : DateTimeOffset.FromUnixTimeSeconds((long)item.Expires.SecondsSince1970).DateTime
        );
    }

    internal static Binding.ActitoUserInboxItem ToNativeInboxItem(ActitoUserInboxItem item)
    {
        return new Binding.ActitoUserInboxItem(
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
