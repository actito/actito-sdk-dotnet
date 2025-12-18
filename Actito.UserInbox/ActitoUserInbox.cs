using ActitoSdk.Core.Models;
using ActitoSdk.UserInbox.Core.Internal;
using ActitoSdk.UserInbox.Core.Models;

namespace ActitoSdk.UserInbox;

public static class ActitoUserInbox
{
    private static readonly Lazy<IActitoUserInboxPlatform> Implementation = new(() =>
    {
        var instance = CreateActito();
        instance.Initialize();

        return instance;
    });

    private static IActitoUserInboxPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    /// <summary>
    /// Parses a JSON string to produce a <see cref="ActitoUserInboxResponse"/>.
    ///
    /// This method takes a raw JSON string and converts it into a structured <see cref="ActitoUserInboxResponse"/>.
    /// </summary>
    /// <param name="json">The JSON string representing the user inbox response.</param>
    /// <returns>
    /// A promise that resolves to a <see cref="ActitoUserInboxResponse"/> object parsed from the provided JSON string.
    /// </returns>
    public static Task<ActitoUserInboxResponse> ParseResponseAsync(string json) => Platform.ParseResponseAsync(json);

    /// <summary>
    /// Parses a <see cref="HttpRequestMessage"/> to produce a <see cref="ActitoUserInboxResponse"/>.
    /// </summary>
    /// <param name="response">The <see cref="HttpRequestMessage"/> representing the user inbox response.</param>
    /// <returns>
    /// A promise that resolves to a <see cref="ActitoUserInboxResponse"/> object parsed from the provided HTTP request message.
    /// </returns>
    public static Task<ActitoUserInboxResponse> ParseResponseAsync(HttpResponseMessage response) =>
        Platform.ParseResponseAsync(response);

    /// <summary>
    /// Opens an inbox item and retrieves its associated notification.
    ///
    /// This function opens the provided <see cref="ActitoUserInboxItem"/> and returns the associated <see cref="ActitoNotification"/>.
    /// This operation marks the item as read.
    /// </summary>
    /// <param name="item">The <see cref="ActitoUserInboxItem"/> to be opened.</param>
    /// <returns>
    /// A task that resolves to the <see cref="ActitoNotification"/> associated with the opened inbox item.
    /// </returns>
    public static Task<ActitoNotification> OpenAsync(ActitoUserInboxItem item) => Platform.OpenAsync(item);

    /// <summary>
    /// Marks an inbox item as read.
    ///
    /// This function updates the status of the provided <see cref="ActitoUserInboxItem"/> to read.
    /// </summary>
    /// <param name="item">The <see cref="ActitoUserInboxItem"/> to mark as read.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully marked as read.
    /// </returns>
    public static Task MarkAsReadAsync(ActitoUserInboxItem item) => Platform.MarkAsReadAsync(item);

    /// <summary>
    /// Removes an inbox item from the user's inbox.
    ///
    /// This function deletes the provided <see cref="ActitoUserInboxItem"/> from the user's inbox.
    /// </summary>
    /// <param name="item"> The <see cref="ActitoUserInboxItem"/> to be removed.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully removed.
    /// </returns>
    public static Task RemoveAsync(ActitoUserInboxItem item) => Platform.RemoveAsync(item);


    private static IActitoUserInboxPlatform CreateActito()
    {
#if ANDROID
        return new Android.ActitoUserInboxPlatformAndroid();
#elif IOS
        return new iOS.ActitoUserInboxPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Actito.");
    }
}
