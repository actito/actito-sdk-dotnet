namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// iOS-specific options that control how a notification is presented
/// when the app is in the foreground.
/// </summary>
public enum ActitoPresentationOptions
{
    /// <summary>
    /// Displays the notification as a banner.
    /// </summary>
    Banner,

    /// <summary>
    /// Displays the notification as an alert.
    /// </summary>
    Alert,

    /// <summary>
    /// Displays the notification in the Notification Center.
    /// </summary>
    List,

    /// <summary>
    /// Updates the app icon badge when the notification is delivered.
    /// </summary>
    Badge,

    /// <summary>
    /// Plays a sound when the notification is delivered.
    /// </summary>
    Sound,
}
