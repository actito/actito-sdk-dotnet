namespace ActitoSdk.Inbox.Core.Events;

public class ActitoBadgeUpdatedEventArgs(int badge) : EventArgs
{
    public int Badge { get; } = badge;
}
