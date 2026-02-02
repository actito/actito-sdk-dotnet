namespace ActitoSdk.InAppMessaging.Core.Models;

/// <summary>
/// Represents an in-app message delivered by Actito.
/// </summary>
/// <remarks>
/// An <see cref="ActitoInAppMessage"/> defines content that can be displayed directly within
/// the application. Messages may include text, images, and actions for user interaction.
/// </remarks>
public class ActitoInAppMessage
{
    /// <summary>
    /// Unique identifier of the in-app message.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Human-readable name of the message.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Type of the message.
    /// </summary>
    /// <remarks>
    /// Supported message types:
    /// 
    /// - `re.notifica.inappmessage.Banner`
    /// - `re.notifica.inappmessage.Card`
    /// - `re.notifica.inappmessage.Fullscreen`
    /// </remarks>
    public string Type { get; }

    /// <summary>
    /// List of contexts where the message should be displayed.
    /// </summary>
    /// <remarks>
    /// Supported contexts:
    /// 
    /// - `launch` — displayed when the application is launched
    /// - `foreground` — displayed while the application is in the foreground
    /// </remarks>
    public IList<string> Context { get; }

    /// <summary>
    /// Optional title of the message.
    /// </summary>
    public string? Title { get; }

    /// <summary>
    /// Optional body text of the message.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Optional portrait image URL associated with the message.
    /// </summary>
    public string? Image { get; }

    /// <summary>
    /// Optional landscape image URL associated with the message.
    /// </summary>
    public string? LandscapeImage { get; }

    /// <summary>
    /// Delay before displaying the message, in seconds.
    /// </summary>
    public int DelaySeconds { get; }

    /// <summary>
    /// Optional primary action associated with the message.
    /// </summary>
    public ActitoInAppMessageAction? PrimaryAction { get; }

    /// <summary>
    /// Optional secondary action associated with the message.
    /// </summary>
    public ActitoInAppMessageAction? SecondaryAction { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoInAppMessage"/>.
    /// </summary>
    public ActitoInAppMessage(string id, string name, string type, IList<string> context, string? title,
        string? message, string? image, string? landscapeImage, int delaySeconds,
        ActitoInAppMessageAction? primaryAction, ActitoInAppMessageAction? secondaryAction)
    {
        Id = id;
        Name = name;
        Type = type;
        Context = context;
        Title = title;
        Message = message;
        Image = image;
        LandscapeImage = landscapeImage;
        DelaySeconds = delaySeconds;
        PrimaryAction = primaryAction;
        SecondaryAction = secondaryAction;
    }
}

/// <summary>
/// Represents an action associated with an in-app message.
/// </summary>
/// <remarks>
/// An <see cref="ActitoInAppMessageAction"/> defines a user interaction option for an in-app
/// message, such as opening a URL or performing an operation.
/// </remarks>
public class ActitoInAppMessageAction
{
    /// <summary>
    /// Optional label displayed for the action.
    /// </summary>
    public string? Label { get; }

    /// <summary>
    /// Indicates whether the action is destructive.
    /// </summary>
    public bool Destructive { get; }

    /// <summary>
    /// Optional target URL triggered by the action.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoInAppMessageAction"/>.
    /// </summary>
    public ActitoInAppMessageAction(string? label, bool destructive, string? url)
    {
        Label = label;
        Destructive = destructive;
        Url = url;
    }
}
