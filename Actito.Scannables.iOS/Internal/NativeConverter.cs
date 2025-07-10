using ActitoSdk.iOS.Internal;
using ActitoSdk.Scannables.Core.Models;

namespace ActitoSdk.Scannables.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoScannable FromNativeScannable(Binding.ActitoScannable scannable)
    {
        return new ActitoScannable(
            id: scannable.ScannableId,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : ActitoNativeConverter.FromNativeNotification(scannable.Notification)
        );
    }
}
