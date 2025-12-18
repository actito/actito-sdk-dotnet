using System.Reflection;
using Auth0.OidcClient;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sample.UserInbox.Network;
using Sample.UserInbox.ViewModels;

namespace Sample.UserInbox;

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
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var auth0Configuration = GetAuth0Configuration();
        var domain = auth0Configuration["USER_INBOX_CLIENT_DOMAIN"];
        var clientId = auth0Configuration["USER_INBOX_CLIENT_ID"];
        var redirectUri = auth0Configuration["USER_INBOX_REDIRECT_URI"];

        if (domain.IsNullOrEmpty() || clientId.IsNullOrEmpty() || redirectUri.IsNullOrEmpty())
        {
            throw new InvalidOperationException("Missing Auth0 environment variables.");
        }

        builder.Services.AddTransient<UserInboxService>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddScoped<InboxViewModel>();
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = domain,
            ClientId = clientId,
            RedirectUri = redirectUri,
            PostLogoutRedirectUri = redirectUri
        }));

        return builder.Build();
    }

    internal static IConfigurationSection GetUserInboxConfiguration()
    {
        return GetRootConfiguration().GetRequiredSection("UserInbox");
    }

    private static IConfigurationSection GetAuth0Configuration()
    {
        return GetRootConfiguration().GetRequiredSection("Auth0");
    }

    private static IConfigurationRoot GetRootConfiguration()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Sample.UserInbox.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        return config;
    }
}
