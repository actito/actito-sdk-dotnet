using ActitoSdk.Inbox.Core.Models;

namespace ActitoSdk.Inbox.Core.Events;

public class ActitoInboxUpdatedEventArgs(IList<ActitoInboxItem> items) : EventArgs
{
    public IList<ActitoInboxItem> Items { get; } = items;
}
