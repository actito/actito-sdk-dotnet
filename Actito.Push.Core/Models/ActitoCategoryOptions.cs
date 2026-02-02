namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// iOS-specific notification category options.
/// </summary>
/// <remarks>
/// These options configure the behavior and presentation of notification
/// categories on iOS.
/// </remarks>
public enum ActitoCategoryOptions
{
    /// <summary>
    /// Adds a custom dismiss action to the notification category.
    /// </summary>
    CustomDismissAction,

    /// <summary>
    /// Allows notifications in this category to be displayed in CarPlay.
    /// </summary>
    AllowInCarPlay,

    /// <summary>
    /// Displays the notification title when previews are hidden.
    /// </summary>
    HiddenPreviewsShowTitle,

    /// <summary>
    /// Displays the notification subtitle when previews are hidden.
    /// </summary>
    HiddenPreviewsShowSubtitle,

    /// <summary>
    /// Allows notifications in this category to be announced using voice assistance.
    /// </summary>
    AllowAnnouncement,
}
