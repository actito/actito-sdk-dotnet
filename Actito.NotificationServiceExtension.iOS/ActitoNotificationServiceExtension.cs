using UserNotifications;
using NativeActito = ActitoSdk.NotificationServiceExtension.iOS.Binding.ActitoNotificationServiceExtensionNativeBinding;

namespace ActitoSdk.NotificationServiceExtension.iOS;

public static class ActitoNotificationServiceExtension
{
    /// <summary>
    /// Handles the processing of a notification request and returns the resulting notification content.
    /// </summary>
    /// <param name="request">The <see cref="UNNotificationRequest"/> containing the request details to be processed.</param>
    /// <returns>
    /// A task that resolves to the <see cref="UNNotificationContent"/> representing the processed notification content.
    /// </returns>
    public static Task<UNNotificationContent> HandleNotificationRequest(UNNotificationRequest request)
    {
        TaskCompletionSource<UNNotificationContent> completion = new();

        NativeActito.HandleNotificationRequest(
            request,
            content => completion.TrySetResult(content),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
