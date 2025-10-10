using CoreFoundation;
using Foundation;
using ActitoSdk;
using ActitoSdk.Push;
using ActitoSdk.Push.Core.Models;
using UIKit;

namespace Sample.UserInbox;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate, IUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var logger = new CoreFoundation.OSLog(subsystem: "com.foo.maui", category: "category");
        logger.Log(OSLogLevel.Error, "FinishedLaunching");

        Task.Run(async () =>
        {
            try
            {
                ActitoPush.SetPresentationOptions(new List<ActitoPresentationOptions>
                {
                    ActitoPresentationOptions.Banner,
                    ActitoPresentationOptions.Banner,
                    ActitoPresentationOptions.Sound
                });

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

    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
        if (Actito.HandleTestDeviceUrl(url))
        {
            return true;
        }

        if (Actito.HandleDynamicLinkUrl(url))
        {
            return true;
        }

        HandleAppLink(url.AbsoluteString);
        return false;
    }

    public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity,
        UIApplicationRestorationHandler completionHandler)
    {
        var url = userActivity.WebPageUrl;
        if (url == null) return false;

        if (Actito.HandleTestDeviceUrl(url))
        {
            return true;
        }

        return Actito.HandleDynamicLinkUrl(url);
    }

    private static void HandleAppLink(string url)
    {
        if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            App.Current?.SendOnAppLinkRequestReceived(uri);
    }
}
