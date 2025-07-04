using ActitoSdk.Loyalty.Core.Internal;
using ActitoSdk.Loyalty.Core.Models;

namespace ActitoSdk.Loyalty;

public static class ActitoLoyalty
{
    private static readonly Lazy<IActitoLoyaltyPlatform> Implementation = new(() =>
    {
        var instance = CreateActito();
        instance.Initialize();

        return instance;
    });

    private static IActitoLoyaltyPlatform Platform
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
    /// Fetches a pass by its serial number.
    /// </summary>
    /// <param name="serial">The serial number of the pass to be fetched.</param>
    /// <returns>
    /// A task that resolves to the fetched <see cref="ActitoPass"/> corresponding to the given serial number.
    /// </returns>
    public static Task<ActitoPass> FetchPassBySerialAsync(string serial) =>
        Platform.FetchPassBySerialAsync(serial);

    /// <summary>
    /// Fetches a pass by its barcode.
    /// </summary>
    /// <param name="barcode">The barcode of the pass to be fetched.</param>
    /// <returns>
    /// A task that resolves to the fetched <see cref="ActitoPass"/> corresponding to the given barcode.
    /// </returns>
    public static Task<ActitoPass> FetchPassByBarcodeAsync(string barcode) =>
        Platform.FetchPassByBarcodeAsync(barcode);

#if ANDROID
    /// <summary>
    /// Presents a pass to the user.
    /// </summary>
    /// <param name="pass">The <see cref="ActitoPass"/> to be presented to the user.</param>
    /// <param name="activity">The <see cref="Activity"/> context from which the pass presentation will be launched.</param>
    public static void Present(ActitoPass pass, Activity activity) => Platform.Present(pass, activity);
#elif IOS
    /// <summary>
    /// Presents a pass to the user.
    /// </summary>
    /// <param name="pass">The <see cref="ActitoPass"/> to be presented to the user.</param>
    /// <param name="controller">The <see cref="UIViewController"/> from which the pass presentation will be launched.</param>
    public static void Present(ActitoPass pass, UIViewController controller) => Platform.Present(pass, controller);
#endif


    private static IActitoLoyaltyPlatform CreateActito()
    {
#if ANDROID
        return new Android.ActitoLoyaltyPlatformAndroid();
#elif IOS
        return new iOS.ActitoLoyaltyPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Actito.");
    }
}
