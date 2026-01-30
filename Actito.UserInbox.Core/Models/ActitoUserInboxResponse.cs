namespace ActitoSdk.UserInbox.Core.Models;

/// <summary>
/// Represents the response returned when fetching a user's inbox.
/// </summary>
/// <remarks>
/// An <see cref="ActitoUserInboxResponse"/> contains the total number of inbox items,
/// the number of unread items, and the list of items themselves.
/// </remarks>
public class ActitoUserInboxResponse
{
    /// <summary>
    /// Total number of items in the user's inbox.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Number of unread items in the user's inbox.
    /// </summary>
    public int Unread { get; }

    /// <summary>
    /// List of inbox items for the user.
    /// </summary>
    public IList<ActitoUserInboxItem> Items { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoUserInboxResponse"/>.
    /// </summary>
    public ActitoUserInboxResponse(int count, int unread, IList<ActitoUserInboxItem> items)
    {
        Count = count;
        Unread = unread;
        Items = items;
    }
}
