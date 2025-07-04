using ActitoSdk.InAppMessaging.Core.Models;

namespace ActitoSdk.InAppMessaging.iOS.Internal;

internal static class NativeConverter
{
    internal static ActitoInAppMessage FromNativeMessage(
        ActitoSdk.InAppMessaging.iOS.Binding.ActitoInAppMessage message)
    {
        return new ActitoInAppMessage(
            id: message.InAppMessageId,
            name: message.InAppMessageId,
            type: message.Type,
            context: message.Context.ToList(),
            title: message.Title,
            message: message.Message,
            image: message.Image,
            landscapeImage: message.LandscapeImage,
            delaySeconds: message.DelaySeconds.ToInt32(),
            primaryAction: message.PrimaryAction == null ? null : FromNativeMessageAction(message.PrimaryAction),
            secondaryAction: message.SecondaryAction == null ? null : FromNativeMessageAction(message.SecondaryAction)
        );
    }

    internal static ActitoInAppMessageAction FromNativeMessageAction(
        ActitoSdk.InAppMessaging.iOS.Binding.ActitoInAppMessageAction action)
    {
        return new ActitoInAppMessageAction(
            label: action.Label,
            destructive: action.Destructive,
            url: action.Url
        );
    }
}
