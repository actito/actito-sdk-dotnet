using ActitoSdk.Android.Internal;
using ActitoSdk.Scannables.Core.Models;

namespace ActitoSdk.Scannables.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoScannable FromNativeScannable(Binding.Models.ActitoScannable scannable)
    {
        return new ActitoScannable(
            id: scannable.Id,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : ActitoNativeConverter.FromNativeNotification(scannable.Notification)
        );
    }

    internal static Binding.Models.ActitoScannable ToNativeScannable(ActitoScannable scannable)
    {
        return new Binding.Models.ActitoScannable(
            id: scannable.Id,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : ActitoNativeConverter.ToNativeNotification(scannable.Notification)
        );
    }
}
