using ActitoSdk.NotificationServiceExtension.iOS;
using UserNotifications;

namespace NotificationServiceExtension;

[Register("NotificationService")]
public class NotificationService : UNNotificationServiceExtension
{
    protected NotificationService(IntPtr handle) : base(handle)
    {
        // Note: this .ctor should not contain any initialization logic.
    }

    public override void DidReceiveNotificationRequest(
        UNNotificationRequest request,
        Action<UNNotificationContent> contentHandler
    )
    {
        Task.Run(async () =>
        {
            try
            {
                var content = await ActitoNotificationServiceExtension.HandleNotificationRequest(request);
                contentHandler(content);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to handle the notification request: {e.Message}");
                contentHandler(request.Content);
            }
        });
    }
}
