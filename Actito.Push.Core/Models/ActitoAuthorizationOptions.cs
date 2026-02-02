namespace ActitoSdk.Push.Core.Models;

/// <summary>
/// iOS-specific authorization options used when requesting notification permissions.
/// </summary>
/// <remarks>
/// These options map directly to iOS notification authorization settings.
/// </remarks>
public enum ActitoAuthorizationOptions
{
    /// <summary>
    /// Allows the app to display alert notifications.
    /// </summary>
    Alert,

    /// <summary>
    /// Allows the app to update the app icon badge.
    /// </summary>
    Badge,

    /// <summary>
    /// Allows the app to play notification sounds.
    /// </summary>
    Sound,

    /// <summary>
    /// Allows notifications to be displayed in CarPlay.
    /// </summary>
    CarPlay,

    /// <summary>
    /// Allows the app to provide custom notification settings.
    /// </summary>
    ProvidesAppNotificationSettings,

    /// <summary>
    /// Allows the ability to post noninterrupting notifications 
    /// provisionally to the Notification Center.
    /// </summary>
    Provisional,

    /// <summary>
    /// Allows the app to play sounds for critical alerts.
    /// </summary>
    CriticalAlert,

    /// <summary>
    /// Allows notifications to be announced using voice assistance.
    /// </summary>
    Announcement,
}
