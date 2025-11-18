using ActitoSdk.Core.Models;
using ActitoSdk.Inbox.Core.Events;
using ActitoSdk.Inbox.Core.Models;

namespace ActitoSdk.Inbox.Core.Internal;

public interface IActitoInboxPlatform
{
    void Initialize();

    event EventHandler<ActitoInboxUpdatedEventArgs> InboxUpdated;

    event EventHandler<ActitoBadgeUpdatedEventArgs> BadgeUpdated;

    IList<ActitoInboxItem> Items { get; }

    int Badge { get; }

    Task RefreshAsync();

    Task<ActitoNotification> OpenAsync(ActitoInboxItem item);

    Task MarkAsReadAsync(ActitoInboxItem item);

    Task MarkAllAsReadAsync();

    Task RemoveAsync(ActitoInboxItem item);

    Task ClearAsync();
}
