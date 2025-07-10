namespace ActitoSdk.Push.Core.Models;

public class ActitoSystemNotification
{
    public string Id { get; }
    public string Type { get; }
    public IDictionary<string, object?> Extra { get; }

    public ActitoSystemNotification(string id, string type, IDictionary<string, object?> extra)
    {
        Id = id;
        Type = type;
        Extra = extra;
    }
}
