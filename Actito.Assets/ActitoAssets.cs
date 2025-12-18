using ActitoSdk.Assets.Core.Internal;
using ActitoSdk.Assets.Core.Models;

namespace ActitoSdk.Assets;

public static class ActitoAssets
{
    private static readonly Lazy<IActitoAssetsPlatform> Implementation = new(() =>
    {
        var instance = CreateActito();
        instance.Initialize();

        return instance;
    });

    private static IActitoAssetsPlatform Platform
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
    /// Fetches a list of <see cref="ActitoAsset"/> for a specified group.
    /// </summary>
    /// <param name="group">The name of the group whose assets are to be fetched.</param>
    /// <returns>
    /// A task that resolves to a list of <see cref="ActitoAsset"/> belonging to the specified group.
    /// </returns>
    public static Task<IList<ActitoAsset>> FetchAsync(string group) => Platform.FetchAsync(group);


    private static IActitoAssetsPlatform CreateActito()
    {
#if ANDROID
        return new Android.ActitoAssetsPlatformAndroid();
#elif IOS
        return new iOS.ActitoAssetsPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Actito.");
    }
}
