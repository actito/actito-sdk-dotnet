namespace ActitoSdk.Core.Models;

public class ActitoDevice
{
    public string Id { get; }
    public string? UserId { get; }
    public string? UserName { get; }
    public double TimeZoneOffset { get; }
    public ActitoDoNotDisturb? Dnd { get; }
    public IDictionary<string, string> UserData { get; }

    public ActitoDevice(string id, string? userId, string? userName, double timeZoneOffset, ActitoDoNotDisturb? dnd, IDictionary<string, string> userData)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        TimeZoneOffset = timeZoneOffset;
        Dnd = dnd;
        UserData = userData;
    }
}
