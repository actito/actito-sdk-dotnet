namespace ActitoSdk.Push.Core.Models;

public class ActitoPushSubscription
{
    public string? Token { get; }

    public ActitoPushSubscription(string? token)
    {
        Token = token;
    }
}
