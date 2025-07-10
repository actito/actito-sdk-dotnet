namespace ActitoSdk.UserInbox.Core.Models;

public class ActitoUserInboxResponse
{
    public int Count { get; }
    public int Unread { get; }
    public IList<ActitoUserInboxItem> Items { get; }

    public ActitoUserInboxResponse(int count, int unread, IList<ActitoUserInboxItem> items)
    {
        Count = count;
        Unread = unread;
        Items = items;
    }
}
