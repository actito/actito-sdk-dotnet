using ActitoSdk.Android.Internal;
using ActitoSdk.Inbox.Core.Events;
using ActitoSdk.Inbox.Core.Internal;
using ActitoSdk.Inbox.Core.Models;
using AndroidX.Lifecycle;
using ActitoSdk.Core.Models;
using ActitoSdk.Inbox.Android.Internal;
using NativeActito = ActitoSdk.Inbox.Android.Binding.ActitoInbox;

namespace ActitoSdk.Inbox.Android;

public class ActitoInboxPlatformAndroid : IActitoInboxPlatform
{
    private IObserver? _itemsObserver;
    private IObserver? _badgeObserver;

    public void Initialize()
    {
        ObserveInboxItems();
        ObserveBadge();
    }

    public event EventHandler<ActitoInboxUpdatedEventArgs>? InboxUpdated;
    public event EventHandler<ActitoBadgeUpdatedEventArgs>? BadgeUpdated;

    public IList<ActitoInboxItem> Items =>
        NativeActito.Items
            .ToEnumerable<Binding.Models.ActitoInboxItem>()
            .Cast<Binding.Models.ActitoInboxItem>()
            .Select(NativeConverter.FromNativeInboxItem)
            .ToList();

    public int Badge => NativeActito.Badge;

    public async Task RefreshAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Refresh(callback);

        await callback.Task;
    }

    public async Task<ActitoNotification> OpenAsync(ActitoInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Open(NativeConverter.ToNativeInboxItem(item), callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (ActitoSdk.Android.Binding.Models.ActitoNotification)result;

        return ActitoNativeConverter.FromNativeNotification(notification);
    }

    public async Task MarkAsReadAsync(ActitoInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.MarkAsRead(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task MarkAllAsReadAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.MarkAllAsRead(callback);

        await callback.Task;
    }

    public async Task RemoveAsync(ActitoInboxItem item)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Remove(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task ClearAsync()
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Clear(callback);

        await callback.Task;
    }


    private void ObserveInboxItems()
    {
        if (_itemsObserver != null)
        {
            NativeActito.ObservableItems.RemoveObserver(_itemsObserver);
        }

        _itemsObserver = new InboxItemsObserver(this);
        NativeActito.ObservableItems.ObserveForever(_itemsObserver);
    }

    private void ObserveBadge()
    {
        if (_badgeObserver != null)
        {
            NativeActito.ObservableBadge.RemoveObserver(_badgeObserver);
        }

        _badgeObserver = new BadgeObserver(this);
        NativeActito.ObservableBadge.ObserveForever(_badgeObserver);
    }


    private class InboxItemsObserver : Java.Lang.Object, IObserver
    {
        private readonly ActitoInboxPlatformAndroid _platform;

        internal InboxItemsObserver(ActitoInboxPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value is not Java.Util.ICollection items) return;

            _platform.InboxUpdated?.Invoke(
                _platform,
                new ActitoInboxUpdatedEventArgs(
                    items: items
                        .ToEnumerable<Binding.Models.ActitoInboxItem>()
                        .Cast<Binding.Models.ActitoInboxItem>()
                        .Select(NativeConverter.FromNativeInboxItem)
                        .ToList()
                )
            );
        }
    }

    private class BadgeObserver : Java.Lang.Object, IObserver
    {
        private readonly ActitoInboxPlatformAndroid _platform;

        internal BadgeObserver(ActitoInboxPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value is not Java.Lang.Integer badge) return;

            _platform.BadgeUpdated?.Invoke(
                _platform,
                new ActitoBadgeUpdatedEventArgs(
                    badge: badge.IntValue()
                )
            );
        }
    }
}
