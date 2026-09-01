using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>();

		// UseMauiCommunityToolkit requires iOS/MacCatalyst 15+; this app supports iOS 13+, so skip it
		// there rather than crash on older devices.
#if IOS
		if (OperatingSystem.IsIOSVersionAtLeast(15))
			builder.UseMauiCommunityToolkit();
#else
		builder.UseMauiCommunityToolkit();
#endif

		builder.ConfigureFonts(fonts =>
		{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}
}
