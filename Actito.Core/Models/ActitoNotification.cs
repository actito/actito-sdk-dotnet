namespace ActitoSdk.Core.Models;

/// <summary>
/// Represents a notification delivered by Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoNotification"/> contains the payload of a notification, including
/// its content, actions, attachments, and additional metadata.
/// Notifications may be partial, meaning that only a subset of fields is provided
/// and additional data may need to be fetched.
/// </remarks>
public class ActitoNotification
{
    /// <summary>
    /// Indicates whether this notification is partial.
    /// </summary>
    /// <remarks>
    /// When `true`, the notification does not contain the full payload.
    /// </remarks>
    public bool Partial { get; }

    /// <summary>
    /// Unique identifier of the notification.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Type of the notification.
    /// </summary>
    /// <remarks>
    /// This value is defined by Actito and is used to distinguish different
    /// notification behaviors.
    /// 
    /// Supported notification types:
    /// 
    /// - `re.notifica.notification.None`
    /// - `re.notifica.notification.Alert`
    /// - `re.notifica.notification.InAppBrowser`
    /// - `re.notifica.notification.WebView`
    /// - `re.notifica.notification.URL`
    /// - `re.notifica.notification.URLResolver`
    /// - `re.notifica.notification.URLScheme`
    /// - `re.notifica.notification.Image`
    /// - `re.notifica.notification.Video`
    /// - `re.notifica.notification.Map`
    /// - `re.notifica.notification.Rate`
    /// - `re.notifica.notification.Passbook`
    /// - `re.notifica.notification.Store`
    /// </remarks>
    public string Type { get; }

    /// <summary>
    /// Timestamp indicating when the notification was generated.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Optional title displayed in the notification.
    /// </summary>
    public string? Title { get; }

    /// <summary>
    /// Optional subtitle displayed in the notification.
    /// </summary>
    public string? Subtitle { get; }

    /// <summary>
    /// Main message body of the notification.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Structured content elements associated with the notification.
    /// </summary>
    public IList<ActitoNotificationContent> Content { get; }

    /// <summary>
    /// List of actions that can be performed from the notification.
    /// </summary>
    public IList<ActitoNotificationAction> Actions { get; }

    /// <summary>
    /// List of attachments included with the notification.
    /// </summary>
    public IList<ActitoNotificationAttachment> Attachments { get; }

    /// <summary>
    /// Collection of key-value pairs used to add extra information to the notification.
    /// </summary>
    public IDictionary<string, object> Extra { get; }

    /// <summary>
    /// Optional identifier of the target content related to the notification.
    /// </summary>
    public string? TargetContentIdentifier { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotification"/>.
    /// </summary>
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

/// <summary>
/// Represents a structured content element within a notification.
/// </summary>
public class ActitoNotificationContent
{
    /// <summary>
    /// The content type identifier.
    /// </summary>
    /// <remarks>
    /// Supported content types:
    /// 
    /// - `re.notifica.content.HTML`
    /// - `re.notifica.content.PKPass`
    /// - `re.notifica.content.GooglePlayDetails`
    /// - `re.notifica.content.GooglePlayDeveloper`
    /// - `re.notifica.content.GooglePlaySearch`
    /// - `re.notifica.content.GooglePlayCollection`
    /// - `re.notifica.content.AppGalleryDetails`
    /// - `re.notifica.content.AppGallerySearch`
    /// </remarks>
    public string Type { get; }

    /// <summary>
    /// The content payload.
    /// </summary>
    public object Data { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotificationContent"/>.
    /// </summary>
    public ActitoNotificationContent(string type, object data)
    {
        Type = type;
        Data = data;
    }
}

/// <summary>
/// Represents an action that can be triggered from a notification.
/// </summary>
public class ActitoNotificationAction
{
    /// <summary>
    /// Type of the action.
    /// </summary>
    /// <remarks>
    /// Supported action types:
    /// 
    /// - `re.notifica.action.App`
    /// - `re.notifica.action.Browser`
    /// - `re.notifica.action.Callback`
    /// - `re.notifica.action.Custom`
    /// - `re.notifica.action.Mail`
    /// - `re.notifica.action.SMS`
    /// - `re.notifica.action.Telephone`
    /// - `re.notifica.action.InAppBrowser`
    /// </remarks>
    public string Type { get; }

    /// <summary>
    /// User-visible label of the action.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Optional target associated with the action.
    /// </summary>
    public string? Target { get; }

    /// <summary>
    /// Whether the action requires keyboard input.
    /// </summary>
    public bool Keyboard { get; }

    /// <summary>
    /// Whether the action requires camera input.
    /// </summary>
    public bool Camera { get; }

    /// <summary>
    /// Whether the action is destructive.
    /// </summary>
    public bool? Destructive { get; }

    /// <summary>
    /// Optional platform-specific icon configuration for the action.
    /// </summary>
    public ActitoNotificationActionIcon? Icon { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotificationAction"/>.
    /// </summary>
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

/// <summary>
/// Defines platform-specific icons for a notification action.
/// </summary>
public class ActitoNotificationActionIcon
{
    /// <summary>
    /// Resource identifier for Android.
    /// </summary>
    public string? Android { get; }

    /// <summary>
    /// Resource identifier for iOS.
    /// </summary>
    public string? IOS { get; }

    /// <summary>
    /// Resource identifier for Web.
    /// </summary>
    public string? Web { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotificationActionIcon"/>.
    /// </summary>
    public ActitoNotificationActionIcon(string? android, string? ios, string? web)
    {
        Android = android;
        IOS = ios;
        Web = web;
    }
}

/// <summary>
/// Represents an attachment included with a notification.
/// </summary>
public class ActitoNotificationAttachment
{
    /// <summary>
    /// MIME type of the attachment.
    /// </summary>
    public string MimeType { get; }

    /// <summary>
    /// URI pointing to the attachment resource.
    /// </summary>
    public string Uri { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotificationActionIcon"/>.
    /// </summary>
    public ActitoNotificationAttachment(string mimeType, string uri)
    {
        MimeType = mimeType;
        Uri = uri;
    }
}
