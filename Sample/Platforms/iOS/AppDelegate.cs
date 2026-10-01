using CoreFoundation;
using Foundation;
using ActitoSdk;
using ActitoSdk.Push;
using ActitoSdk.Push.Core.Models;
using UIKit;

namespace Sample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate, IUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var logger = new CoreFoundation.OSLog(subsystem: "com.foo.maui", category: "category");
        logger.Log(OSLogLevel.Error, "FinishedLaunching");

        ActitoPush.SetPresentationOptions(new List<ActitoPresentationOptions>
        {
            ActitoPresentationOptions.Alert,
            ActitoPresentationOptions.Banner,
            ActitoPresentationOptions.Sound
        });

        Task.Run(async () =>
        {
            try
            {
                await Actito.LaunchAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to launch Actito: {e.Message}");
            }
        });

        return base.FinishedLaunching(application, launchOptions);
    }

    public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
    {
        ActitoPush.RegisteredForRemoteNotifications(application, deviceToken);
    }

    public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
    {
        ActitoPush.FailedToRegisterForRemoteNotifications(application, error);
    }

    public void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    )
    {
        ActitoPush.DidReceiveRemoteNotification(application, userInfo, completionHandler);
    }
}
