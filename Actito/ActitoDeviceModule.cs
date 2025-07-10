using ActitoSdk.Core.Internal;
using ActitoSdk.Core.Models;

namespace ActitoSdk;

public class ActitoDeviceModule
{
    private readonly IActitoPlatform _platform;

    internal ActitoDeviceModule(IActitoPlatform platform)
    {
        _platform = platform;
    }

    /// <summary>
    /// Provides the current registered device information.
    /// </summary>
    public ActitoDevice? CurrentDevice => _platform.CurrentDevice;

    /// <summary>
    /// Provides the preferred language of the current device for notifications and messages,
    /// or <c>null</c> if no preferred language is set.
    /// </summary>
    public string? PreferredLanguage => _platform.PreferredLanguage;

    /// <summary>
    /// Updates the preferred language setting for the device.
    /// </summary>
    /// <param name="language">The preferred language code.</param>
    /// <returns>
    /// A task that resolves when the preferred language has been successfully updated.
    /// </returns>
    public Task UpdatePreferredLanguageAsync(string? language) => _platform.UpdatePreferredLanguageAsync(language);

    /// <summary>
    /// Updates the user information for the device.
    /// </summary>
    /// <param name="userId">Optional user identifier.</param>
    /// <param name="userName">Optional username.</param>
    /// <returns>
    /// A task that resolves when the user information has been successfully updated.
    /// </returns>
    public Task UpdateUserAsync(string? userId, string? userName) => _platform.UpdateUserAsync(userId, userName);

    /// <summary>
    /// Fetches the tags associated with the device.
    /// </summary>
    /// <returns>
    /// A task that resolves to a list of tags currently associated with the device.
    /// </returns>
    public Task<IList<string>> FetchTagsAsync() => _platform.FetchTagsAsync();

    /// <summary>
    /// Adds a single tag to the device.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    /// <returns>
    /// A task that resolves when the tag has been successfully added to the device.
    /// </returns>
    public Task AddTagAsync(string tag) => _platform.AddTagAsync(tag);

    /// <summary>
    /// Adds multiple tags to the device.
    /// </summary>
    /// <param name="tags">A list of tags to add.</param>
    /// <returns>
    /// A task that resolves when all the tags have been successfully added to the device.
    /// </returns>
    public Task AddTagsAsync(IList<string> tags) => _platform.AddTagsAsync(tags);

    /// <summary>
    /// Removes a specific tag from the device.
    /// </summary>
    /// <param name="tag">The tag to remove.</param>
    /// <returns>
    /// A task that resolves when the tag has been successfully removed from the device.
    /// </returns>
    public Task RemoveTagAsync(string tag) => _platform.RemoveTagAsync(tag);

    /// <summary>
    /// Removes multiple tags from the device.
    /// </summary>
    /// <param name="tags">A list of tags to remove.</param>
    /// <returns>
    /// A task that resolves when all the specified tags have been successfully removed from the device.
    /// </returns>
    public Task RemoveTagsAsync(IList<string> tags) => _platform.RemoveTagsAsync(tags);

    /// <summary>
    /// Clears all tags from the device.
    /// </summary>
    /// <returns>
    /// A task that resolves when all tags have been successfully cleared from the device.
    /// </returns>
    public Task ClearTagsAsync() => _platform.ClearTagsAsync();

    /// <summary>
    /// Fetches the "Do Not Disturb" (DND) settings for the device.
    /// </summary>
    /// <returns>
    /// A task that resolves to the current <see cref="ActitoDoNotDisturb"/> settings, or <c>null</c> if none are set.
    /// </returns>
    public Task<ActitoDoNotDisturb?> FetchDoNotDisturbAsync() => _platform.FetchDoNotDisturbAsync();

    /// <summary>
    /// Updates the "Do Not Disturb" (DND) settings for the device.
    /// </summary>
    /// <param name="dnd">The new <see cref="ActitoDoNotDisturb"/> settings to apply.</param>
    /// <returns>
    /// A task that resolves when the DND settings have been successfully updated.
    /// </returns>
    public Task UpdateDoNotDisturbAsync(ActitoDoNotDisturb dnd) => _platform.UpdateDoNotDisturbAsync(dnd);

    /// <summary>
    /// Clears the "Do Not Disturb" (DND) settings for the device.
    /// </summary>
    /// <returns>
    /// A task that resolves when the DND settings have been successfully cleared.
    /// </returns>
    public Task ClearDoNotDisturbAsync() => _platform.ClearDoNotDisturbAsync();

    /// <summary>
    /// Fetches the user data associated with the device.
    /// </summary>
    /// <returns>
    /// A task that resolves to a <see cref="IDictionary{TKey,TValue}"/> object containing the current user data.
    /// </returns>
    public Task<IDictionary<string, string>> FetchUserDataAsync() => _platform.FetchUserDataAsync();

    /// <summary>
    /// Updates the custom user data associated with the device.
    /// </summary>
    /// <param name="userData">The updated user data to associate with the device.</param>
    /// <returns>
    /// A task that resolves when the user data has been successfully updated.
    /// </returns>
    public Task UpdateUserDataAsync(IDictionary<string, string?> userData) => _platform.UpdateUserDataAsync(userData);
}
