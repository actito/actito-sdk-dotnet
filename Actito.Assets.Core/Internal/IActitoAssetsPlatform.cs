using ActitoSdk.Assets.Core.Models;

namespace ActitoSdk.Assets.Core.Internal;

public interface IActitoAssetsPlatform
{
    void Initialize();

    Task<IList<ActitoAsset>> FetchAsync(string group);
}
