using ActitoSdk.Push.UI;
using ActitoSdk.Scannables;
using ActitoSdk.Scannables.Core.Events;

#if IOS
using UIKit;
#endif

namespace Sample.Pages.Scannables;

public partial class ScannablesPage : ContentPage
{
    public ScannablesPage()
    {
        InitializeComponent();

        NfcButton.IsEnabled = ActitoScannables.CanStartNfcScannableSession;

        ActitoScannables.ScannableDetected += OnScannableDetected;
        ActitoScannables.ScannableSessionFailed += OnScannableSessionFailed;
    }

    private void StartQrCodeScannableSession(object sender, EventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not initiate QrCode scannable session. Activity is null.");
            return;
        }

        ActitoScannables.StartQrCodeScannableSession(activity);
#elif IOS
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (viewController == null)
        {
            Console.WriteLine("Could not initiate QrCode scannable session. View controller is null.");
            return;
        }

        ActitoScannables.StartQrCodeScannableSession(viewController, true);
#endif
    }

    private void StartNfcScannableSession(object sender, EventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not initiate Nfc scannable session. Activity is null.");
            return;
        }

        ActitoScannables.StartNfcScannableSession(activity);
#elif IOS
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (viewController == null)
        {
            Console.WriteLine("Could not initiate Nfc scannable session. View controller is null.");
            return;
        }

        ActitoScannables.StartNfcScannableSession();
#endif
    }

    private void OnScannableDetected(object? sender, ActitoScannableDetectedEventArgs e)
    {
        var notification = e.Scannable.Notification;

        if (notification == null)
        {
            Console.WriteLine("Scannable without notification detected.");
            return;
        }

#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not present scannable notification. Activity is null.");
            return;
        }
        
        ActitoPushUI.PresentNotification(notification, activity);

#elif IOS
		var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

		if (rootViewController is null)
		{
			Console.WriteLine("Cannot present a notification with a null root view controller.");
			return;
		}

		if (notification.RequiresViewController())
		{
			var navigationController = new UINavigationController();
			if (navigationController.View is not null)
				navigationController.View.BackgroundColor = UIColor.SystemBackground;

			rootViewController.PresentViewController(
				navigationController,
				true,
				() => ActitoPushUI.PresentNotification(notification, navigationController)
			);
		}
		else
		{
			ActitoPushUI.PresentNotification(notification, rootViewController);
		}

#endif
    }

    private void OnScannableSessionFailed(object? sender, ActitoScannableSessionFailedEventArgs e)
    {
        Console.WriteLine($"Scannable session failed: {e.Error}");
    }
}
