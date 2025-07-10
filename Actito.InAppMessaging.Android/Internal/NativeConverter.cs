using ActitoSdk.InAppMessaging.Core.Models;

namespace ActitoSdk.InAppMessaging.Android.Internal;

internal static class NativeConverter
{
    internal static ActitoInAppMessage FromNativeMessage(
        ActitoSdk.InAppMessaging.Android.Binding.Models.ActitoInAppMessage message)
    {
        return new ActitoInAppMessage(
            id: message.Id,
            name: message.Name,
            type: message.Type,
            context: message.Context,
            title: message.Title,
            message: message.Message,
            image: message.Image,
            landscapeImage: message.LandscapeImage,
            delaySeconds: message.DelaySeconds,
            primaryAction: message.PrimaryAction == null ? null : FromNativeMessageAction(message.PrimaryAction),
            secondaryAction: message.SecondaryAction == null ? null : FromNativeMessageAction(message.SecondaryAction)
        );
    }

    internal static ActitoInAppMessageAction FromNativeMessageAction(
        ActitoSdk.InAppMessaging.Android.Binding.Models.ActitoInAppMessage.Action action)
    {
        return new ActitoInAppMessageAction(
            label: action.Label,
            destructive: action.Destructive,
            url: action.Url
        );
    }
}
