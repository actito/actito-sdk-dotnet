using ActitoSdk.Assets.Core.Internal;
using ActitoSdk.Assets.Core.Models;
using ActitoSdk.Assets.iOS.Internal;

namespace ActitoSdk.Assets.iOS;

public class ActitoAssetsPlatformIos : IActitoAssetsPlatform
{
    private Binding.ActitoAssetsNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<IList<ActitoAsset>> FetchAsync(string group)
    {
        TaskCompletionSource<IList<ActitoAsset>> completion = new();

        _native.Fetch(
            group,
            assets => completion.TrySetResult(assets.ToArray().Select(NativeConverter.FromNativeAsset).ToList()),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
