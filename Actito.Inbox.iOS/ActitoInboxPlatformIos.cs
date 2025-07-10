using ActitoSdk.Core.Models;
using ActitoSdk.Inbox.Core.Events;
using ActitoSdk.Inbox.Core.Internal;
using ActitoSdk.Inbox.Core.Models;
using ActitoSdk.Inbox.iOS.Internal;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.Inbox.iOS;

public class ActitoInboxPlatformIos : IActitoInboxPlatform
{
    private InternalActitoInboxDelegate? _delegate;
    private Binding.ActitoInboxNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalActitoInboxDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<ActitoInboxUpdatedEventArgs>? InboxUpdated;
    public event EventHandler<ActitoBadgeUpdatedEventArgs>? BadgeUpdated;

    public IList<ActitoInboxItem> Items => _native.Items.Select(NativeConverter.FromNativeInboxItem).ToList();

    public int Badge => _native.Badge.ToInt32();

    public void Refresh() => _native.Refresh();

    public Task<ActitoNotification> OpenAsync(ActitoInboxItem item)
    {
        TaskCompletionSource<ActitoNotification> completion = new();

        _native.Open(
            NativeConverter.ToNativeInboxItem(item),
            notification => completion.TrySetResult(ActitoNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAsReadAsync(ActitoInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.MarkAsRead(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAllAsReadAsync()
    {
        TaskCompletionSource completion = new();

        _native.MarkAllAsRead(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveAsync(ActitoInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.Remove(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearAsync()
    {
        TaskCompletionSource completion = new();

        _native.Clear(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }


    private sealed class InternalActitoInboxDelegate : Binding.ActitoInboxNativeBindingDelegate
    {
        private readonly ActitoInboxPlatformIos _platform;

        internal InternalActitoInboxDelegate(ActitoInboxPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidUpdateInbox(Binding.ActitoInboxNativeBinding actitoInbox,
            Binding.ActitoInboxItem[] items)
        {
            _platform.InboxUpdated?.Invoke(
                _platform,
                new ActitoInboxUpdatedEventArgs(
                    items.Select(NativeConverter.FromNativeInboxItem).ToList()
                )
            );
        }

        public override void DidUpdateBadge(Binding.ActitoInboxNativeBinding actitoInbox, nint badge)
        {
            _platform.BadgeUpdated?.Invoke(
                _platform,
                new ActitoBadgeUpdatedEventArgs(badge.ToInt32()
                )
            );
        }
    }
}
