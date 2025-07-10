using ActitoSdk.Android.Internal;
using ActitoSdk.Loyalty.Android.Internal;
using ActitoSdk.Loyalty.Core.Internal;
using ActitoSdk.Loyalty.Core.Models;
using NativeActito = ActitoSdk.Loyalty.Android.Binding.ActitoLoyaltyCompat;

namespace ActitoSdk.Loyalty.Android;

public class ActitoLoyaltyPlatformAndroid : IActitoLoyaltyPlatform
{
    public void Initialize()
    {
    }

    public async Task<ActitoPass> FetchPassBySerialAsync(string serial)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.FetchPassBySerial(serial, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var pass = (ActitoSdk.Loyalty.Android.Binding.Models.ActitoPass)result;

        return NativeConverter.FromNativePass(pass);
    }

    public async Task<ActitoPass> FetchPassByBarcodeAsync(string barcode)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.FetchPassBySerial(barcode, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var pass = (ActitoSdk.Loyalty.Android.Binding.Models.ActitoPass)result;

        return NativeConverter.FromNativePass(pass);
    }

    public void Present(ActitoPass pass, Activity activity)
    {
        NativeActito.Present(activity, NativeConverter.ToNativePass(pass));
    }
}
