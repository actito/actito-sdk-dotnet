using ActitoSdk.Loyalty.Core.Models;

namespace ActitoSdk.Loyalty.Core.Internal;

public interface IActitoLoyaltyPlatform
{
    void Initialize();

    Task<ActitoPass> FetchPassBySerialAsync(string serial);

    Task<ActitoPass> FetchPassByBarcodeAsync(string barcode);

#if ANDROID
    void Present(ActitoPass pass, Activity activity);
#elif IOS
    void Present(ActitoPass pass, UIViewController controller);
#endif
}
