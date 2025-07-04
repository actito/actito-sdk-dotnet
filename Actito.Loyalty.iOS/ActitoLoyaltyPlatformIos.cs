using ActitoSdk.Loyalty.Core.Internal;
using ActitoSdk.Loyalty.Core.Models;
using ActitoSdk.Loyalty.iOS.Internal;

namespace ActitoSdk.Loyalty.iOS;

public class ActitoLoyaltyPlatformIos : IActitoLoyaltyPlatform
{
    private Binding.ActitoLoyaltyNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<ActitoPass> FetchPassBySerialAsync(string serial)
    {
        TaskCompletionSource<ActitoPass> completion = new();

        _native.FetchPassBySerial(
            serial,
            pass => completion.TrySetResult(NativeConverter.FromNativePass(pass)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<ActitoPass> FetchPassByBarcodeAsync(string barcode)
    {
        TaskCompletionSource<ActitoPass> completion = new();

        _native.FetchPassByBarcode(
            barcode,
            pass => completion.TrySetResult(NativeConverter.FromNativePass(pass)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public void Present(ActitoPass pass, UIViewController controller)
    {
        _native.Present(NativeConverter.ToNativePass(pass), controller);
    }
}
