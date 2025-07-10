using ActitoSdk.Android.Internal;
using ActitoSdk.Assets.Core.Models;

namespace ActitoSdk.Assets.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoAsset FromNativeAsset(ActitoSdk.Assets.Android.Binding.Models.ActitoAsset asset)
    {
        return new ActitoAsset(
            title: asset.Title,
            description: asset.Description,
            key: asset.Key,
            url: asset.Url,
            button: asset.GetButton() == null ? null : FromNativeAssetButton(asset.GetButton()!),
            metaData: asset.GetMetaData() == null ? null : FromNativeAssetMetaData(asset.GetMetaData()!),
            extra: ActitoNativeConverter.FromNativeExtraDictionary(asset.Extra)
        );
    }

    private static ActitoAssetButton FromNativeAssetButton(
        ActitoSdk.Assets.Android.Binding.Models.ActitoAsset.Button button)
    {
        return new ActitoAssetButton(
            label: button.Label,
            action: button.Action
        );
    }

    private static ActitoAssetMetaData FromNativeAssetMetaData(
        ActitoSdk.Assets.Android.Binding.Models.ActitoAsset.MetaData metaData)
    {
        return new ActitoAssetMetaData(
            originalFileName: metaData.OriginalFileName,
            contentType: metaData.ContentType,
            contentLength: metaData.ContentLength
        );
    }
}
