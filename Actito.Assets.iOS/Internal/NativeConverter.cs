using ActitoSdk.Assets.Core.Models;
using ActitoSdk.iOS.Internal;

namespace ActitoSdk.Assets.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoAsset FromNativeAsset(ActitoSdk.Assets.iOS.Binding.ActitoAsset asset)
    {
        return new ActitoAsset(
            title: asset.Title,
            description: asset.AssetDescription,
            key: asset.Key,
            url: asset.Url,
            button: asset.Button == null ? null : FromNativeAssetButton(asset.Button),
            metaData: asset.MetaData == null ? null : FromNativeAssetMetaData(asset.MetaData),
            extra: ActitoNativeConverter.FromNativeExtraDictionary(asset.Extra)
        );
    }

    private static ActitoAssetButton FromNativeAssetButton(
        ActitoSdk.Assets.iOS.Binding.ActitoAssetButton button)
    {
        return new ActitoAssetButton(
            label: button.Label,
            action: button.Action
        );
    }

    private static ActitoAssetMetaData FromNativeAssetMetaData(ActitoSdk.Assets.iOS.Binding.ActitoAssetMetaData metaData)
    {
        return new ActitoAssetMetaData(
            originalFileName: metaData.OriginalFileName,
            contentType: metaData.ContentType,
            contentLength: metaData.ContentLength.ToInt32()
        );
    }
}
