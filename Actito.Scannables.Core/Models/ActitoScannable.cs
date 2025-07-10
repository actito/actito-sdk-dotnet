using ActitoSdk.Core.Models;

namespace ActitoSdk.Scannables.Core.Models;

public class ActitoScannable
{
    public string Id { get; }
    public string Name { get; }
    public string Tag { get; }
    public string Type { get; }
    public ActitoNotification? Notification { get; }

    public ActitoScannable(string id, string name, string tag, string type, ActitoNotification? notification)
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Notification = notification;
    }
}
