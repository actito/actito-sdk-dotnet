using ActitoSdk.Core.Models;
using ActitoSdk.Push.UI.Core.Events;

namespace ActitoSdk.Push.UI.Core.Internal;

public interface IActitoPushUIPlatform
{
    void Initialize();

    event EventHandler<ActitoNotificationWillPresentEventArgs> NotificationWillPresent;
    event EventHandler<ActitoNotificationPresentedEventArgs> NotificationPresented;
    event EventHandler<ActitoNotificationFinishedPresentingEventArgs> NotificationFinishedPresenting;
    event EventHandler<ActitoNotificationFailedToPresentEventArgs> NotificationFailedToPresent;
    event EventHandler<ActitoNotificationUrlClickedEventArgs> NotificationUrlClicked;
    event EventHandler<ActitoActionWillExecuteEventArgs> ActionWillExecute;
    event EventHandler<ActitoActionExecutedEventArgs> ActionExecuted;
    event EventHandler<ActitoActionNotExecutedEventArgs> ActionNotExecuted;
    event EventHandler<ActitoActionFailedToExecuteEventArgs> ActionFailedToExecute;
    event EventHandler<ActitoCustomActionReceivedEventArgs> CustomActionReceived;
    
#if ANDROID
    void PresentNotification(ActitoNotification notification, Activity activity);

    void PresentAction(ActitoNotification notification, ActitoNotificationAction action, Activity activity);
#elif IOS
    void PresentNotification(ActitoNotification notification, UIViewController controller);

    void PresentAction(ActitoNotification notification, ActitoNotificationAction action, UIViewController controller);
    
    bool RequiresViewController(ActitoNotification notification);
#endif
}
