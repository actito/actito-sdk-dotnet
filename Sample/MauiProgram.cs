using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if IOS
using ActitoSdk;
using Foundation;
using UIKit;
#endif

namespace Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
			})
			.ConfigureLifecycleEvents(events =>
			{
#if IOS
				events.AddiOS(ios => ios
					.SceneWillConnect((_, _, connectionOptions) =>
					{
						// Handle links that launched the app from a cold start.
						// UrlContexts and UserActivities are annotated as non-null but are null on a regular launch.
						// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
						if (connectionOptions.UrlContexts?.AnyObject is UIOpenUrlContext urlContext)
						{
							HandleUrl(urlContext.Url);
							return;
						}

						// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
						if (connectionOptions.UserActivities?.AnyObject is NSUserActivity userActivity)
						{
							HandleUserActivity(userActivity);
						}
					})
					.SceneOpenUrl((_, urlContexts) =>
					{
						var handled = false;

						foreach (var urlContext in urlContexts)
						{
							if (HandleUrl(urlContext.Url))
								handled = true;
						}

						return handled;
					})
					.SceneContinueUserActivity((_, userActivity) => HandleUserActivity(userActivity))
				);
#endif
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

#if IOS
	private static bool HandleUrl(NSUrl url)
	{
		if (Actito.HandleTestDeviceUrl(url))
		{
			return true;
		}

		if (Actito.HandleDynamicLinkUrl(url))
		{
			return true;
		}

		if (Uri.TryCreate(url.AbsoluteString, UriKind.RelativeOrAbsolute, out var uri))
			App.Current?.SendOnAppLinkRequestReceived(uri);

		return false;
	}

	private static bool HandleUserActivity(NSUserActivity userActivity)
	{
		var url = userActivity.WebPageUrl;
		if (url == null) return false;

		if (Actito.HandleTestDeviceUrl(url))
		{
			return true;
		}

		return Actito.HandleDynamicLinkUrl(url);
	}
#endif
}
