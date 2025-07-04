using ActitoSdk.Core.Models;
using ActitoSdk.UserInbox.Core.Models;

namespace ActitoSdk.UserInbox.Core.Internal;

public interface IActitoUserInboxPlatform
{
    void Initialize();

    Task<ActitoUserInboxResponse> ParseResponseAsync(string json);
    
    Task<ActitoUserInboxResponse> ParseResponseAsync(HttpResponseMessage response);
    
    Task<ActitoNotification> OpenAsync(ActitoUserInboxItem item);

    Task MarkAsReadAsync(ActitoUserInboxItem item);

    Task RemoveAsync(ActitoUserInboxItem item);
}
