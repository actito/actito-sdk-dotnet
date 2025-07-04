using ActitoSdk.Android.Internal;
using ActitoSdk.Assets.Android.Internal;
using ActitoSdk.Assets.Core.Internal;
using ActitoSdk.Assets.Core.Models;
using NativeActito = ActitoSdk.Assets.Android.Binding.ActitoAssetsCompat;

namespace ActitoSdk.Assets.Android;

public class ActitoAssetsPlatformAndroid : IActitoAssetsPlatform
{
    public void Initialize()
    {
    }

    public async Task<IList<ActitoAsset>> FetchAsync(string group)
    {
        var callback = new ActitoAwaitableCallback();
        NativeActito.Fetch(group, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var assets = (System.Collections.IList)result;

        return assets
            .Cast<Binding.Models.ActitoAsset>()
            .Select(NativeConverter.FromNativeAsset)
            .ToList();
    }
}
