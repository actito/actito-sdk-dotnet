namespace ActitoSdk.Core.Models;

public class ActitoNotification
{
    public bool Partial { get; }
    public string Id { get; }
    public string Type { get; }
    public DateTime Time { get; }
    public string? Title { get; }
    public string? Subtitle { get; }
    public string Message { get; }
    public IList<ActitoNotificationContent> Content { get; }
    public IList<ActitoNotificationAction> Actions { get; }
    public IList<ActitoNotificationAttachment> Attachments { get; }
    public IDictionary<string, object> Extra { get; }
    public string? TargetContentIdentifier { get; }

    public ActitoNotification(bool partial, string id, string type, DateTime time, string? title, string? subtitle, string message, IList<ActitoNotificationContent> content, IList<ActitoNotificationAction> actions, IList<ActitoNotificationAttachment> attachments, IDictionary<string, object> extra, string? targetContentIdentifier)
    {
        Partial = partial;
        Id = id;
        Type = type;
        Time = time;
        Title = title;
        Subtitle = subtitle;
        Message = message;
        Content = content;
        Actions = actions;
        Attachments = attachments;
        Extra = extra;
        TargetContentIdentifier = targetContentIdentifier;
    }
}

public class ActitoNotificationContent
{
    public string Type { get; }
    public object Data { get; }

    public ActitoNotificationContent(string type, object data)
    {
        Type = type;
        Data = data;
    }
}

public class ActitoNotificationAction
{
    public string Type { get; }
    public string Label { get; }
    public string? Target { get; }
    public bool Keyboard { get; }
    public bool Camera { get; }
    public bool? Destructive { get; }
    public ActitoNotificationActionIcon? Icon { get; }

    public ActitoNotificationAction(string type, string label, string? target, bool keyboard, bool camera, bool? destructive, ActitoNotificationActionIcon? icon)
    {
        Type = type;
        Label = label;
        Target = target;
        Keyboard = keyboard;
        Camera = camera;
        Destructive = destructive;
        Icon = icon;
    }
}

public class ActitoNotificationActionIcon
{
    public string? Android { get; }
    public string? IOS { get; }
    public string? Web { get; }

    public ActitoNotificationActionIcon(string? android, string? ios, string? web)
    {
        Android = android;
        IOS = ios;
        Web = web;
    }
}

public class ActitoNotificationAttachment
{
    public string MimeType { get; }
    public string Uri { get; }

    public ActitoNotificationAttachment(string mimeType, string uri)
    {
        MimeType = mimeType;
        Uri = uri;
    }
}
