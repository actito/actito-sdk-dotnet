using ActitoSdk.Core.Models;

namespace ActitoSdk.iOS.Internal;

public static class ActitoNativeConverter
{
    #region Decoding from native

    /// <summary>
    /// Create a <see cref="ActitoApplication"/> from the binding object.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static ActitoApplication FromNativeApplication(Binding.ActitoApplication application)
    {
        return new ActitoApplication(
            id: application.ApplicationId,
            name: application.Name,
            category: application.Category,
            appStoreId: null,
            services: application.Services.ToDictionary<KeyValuePair<NSString, NSNumber>, string, bool>(
                item => item.Key.ToString(),
                item => item.Value.BoolValue
            ),
            inboxConfig: application.InboxConfig != null
                ? FromNativeApplicationInboxConfig(application.InboxConfig)
                : null,
            regionConfig: application.RegionConfig != null
                ? FromNativeApplicationRegionConfig(application.RegionConfig)
                : null,
            userDataFields: application.UserDataFields.Select(FromNativeApplicationUserDataField).ToList(),
            actionCategories: application.ActionCategories.Select(FromNativeApplicationActionCategory).ToList()
        );
    }

    private static ActitoApplicationInboxConfig FromNativeApplicationInboxConfig(
        Binding.ActitoApplicationInboxConfig inboxConfig)
    {
        return new ActitoApplicationInboxConfig(
            useInbox: inboxConfig.UseInbox,
            useUserInbox: inboxConfig.UseUserInbox,
            autoBadge: inboxConfig.AutoBadge
        );
    }

    private static ActitoApplicationRegionConfig FromNativeApplicationRegionConfig(
        Binding.ActitoApplicationRegionConfig regionConfig)
    {
        return new ActitoApplicationRegionConfig(
            proximityUUID: regionConfig.ProximityUUID
        );
    }

    private static ActitoApplicationUserDataField FromNativeApplicationUserDataField(
        Binding.ActitoApplicationUserDataField userDataField)
    {
        return new ActitoApplicationUserDataField(
            type: userDataField.Type,
            key: userDataField.Key,
            label: userDataField.Label
        );
    }

    private static ActitoApplicationActionCategory FromNativeApplicationActionCategory(
        Binding.ActitoApplicationActionCategory actionCategory)
    {
        return new ActitoApplicationActionCategory(
            name: actionCategory.Name,
            description: actionCategory.ActionCategoryDescription,
            type: actionCategory.Type,
            actions: actionCategory.Actions.Select(FromNativeNotificationAction).ToList()
        );
    }

    /// <summary>
    /// Create a <see cref="ActitoNotification"/> from the binding object.
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public static ActitoNotification FromNativeNotification(
        Binding.ActitoNotification notification)
    {
        return new ActitoNotification(
            partial: notification.Partial,
            id: notification.NotificationId,
            type: notification.Type,
            time: DateTimeOffset.FromUnixTimeSeconds((long)notification.Time.SecondsSince1970).DateTime,
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(FromNativeNotificationContent).ToList(),
            actions: notification.Actions.Select(FromNativeNotificationAction).ToList(),
            attachments: notification.Attachments.Select(FromNativeNotificationAttachment).ToList(),
            extra: FromNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: notification.TargetContentIdentifier
        );
    }

    private static ActitoNotificationContent FromNativeNotificationContent(
        Binding.ActitoNotificationContent content)
    {
        return new ActitoNotificationContent(
            type: content.Type,
            data: content.Data switch
            {
                NSString s => s.ToString(),
                NSDictionary d => FromNativeExtraDictionary(d),
                _ => new Dictionary<string, object>(),
            }
        );
    }

    public static ActitoNotificationAction FromNativeNotificationAction(
        Binding.ActitoNotificationAction action)
    {
        return new ActitoNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive,
            icon: action.Icon != null ? FromNativeNotificationActionIcon(action.Icon) : null
        );
    }

    private static ActitoNotificationActionIcon FromNativeNotificationActionIcon(
        Binding.ActitoNotificationActionIcon icon)
    {
        return new ActitoNotificationActionIcon(
            android: icon.Android,
            ios: icon.Ios,
            web: icon.Web
        );
    }

    private static ActitoNotificationAttachment FromNativeNotificationAttachment(
        Binding.ActitoNotificationAttachment attachment)
    {
        return new ActitoNotificationAttachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }

    /// <summary>
    /// Create a <see cref="ActitoDynamicLink"/> from the binding object.
    /// </summary>
    /// <param name="dynamicLink"></param>
    /// <returns></returns>
    internal static ActitoDynamicLink FromNativeDynamicLink(Binding.ActitoDynamicLink dynamicLink)
    {
        return new ActitoDynamicLink(
            target: dynamicLink.Target
        );
    }

    /// <summary>
    /// Create a <see cref="ActitoDevice"/> from the binding object.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    internal static ActitoDevice FromNativeDevice(Binding.ActitoDevice device)
    {
        return new ActitoDevice(
            id: device.DeviceId,
            userId: device.UserId,
            userName: device.UserName,
            timeZoneOffset: device.TimeZoneOffset,
            dnd: device.Dnd == null ? null : FromNativeDoNotDisturb(device.Dnd),
            userData: device.UserData.ToDictionary<KeyValuePair<NSString, NSString>, string, string>(
                item => item.Key.ToString(),
                item => item.Value.ToString()
            )
        );
    }

    /// <summary>
    /// Create a <see cref="ActitoDoNotDisturb"/> from the binding object.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static ActitoDoNotDisturb FromNativeDoNotDisturb(Binding.ActitoDoNotDisturb dnd)
    {
        return new ActitoDoNotDisturb(
            start: new ActitoTime(
                hours: (int)dnd.Start.Hours,
                minutes: (int)dnd.Start.Minutes
            ),
            end: new ActitoTime(
                hours: (int)dnd.End.Hours,
                minutes: (int)dnd.End.Minutes
            )
        );
    }

    /// <summary>
    /// Create an "extra" dictionary from the binding representation.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static IDictionary<string, object> FromNativeExtraDictionary(NSDictionary data)
    {
        return data.ToDictionary(
            kvp => kvp.Key.ToString(),
            kvp => FromNativeExtraPrimitive(kvp.Value)
        );
    }

    private static object FromNativeExtraPrimitive(NSObject value)
    {
        return value switch
        {
            NSString s => s.ToString(),
            NSNumber { ObjCType: "i" } n => n.Int32Value,
            NSNumber { ObjCType: "q" } n => n.Int64Value,
            NSNumber { ObjCType: "d" } n => n.DoubleValue,
            NSNumber { ObjCType: "f" } n => n.FloatValue,
            NSNumber { ObjCType: "c" } n => n.BoolValue,
            NSArray a => a.Select(FromNativeExtraPrimitive).ToList(),
            NSDictionary d => FromNativeExtraDictionary(d),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be decoded.")
        };
    }

    #endregion

    #region Enconding to native

    public static Binding.ActitoNotification ToNativeNotification(ActitoNotification notification)
    {
        return new Binding.ActitoNotification(
            partial: notification.Partial,
            notificationId: notification.Id,
            type: notification.Type,
            time: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(notification.Time).ToUnixTimeSeconds()),
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(ToNativeNotificationContent).ToArray(),
            actions: notification.Actions.Select(ToNativeNotificationAction).ToArray(),
            attachments: notification.Attachments.Select(ToNativeNotificationAttachment).ToArray(),
            extra: ToNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: notification.TargetContentIdentifier
        );
    }

    private static Binding.ActitoNotificationContent ToNativeNotificationContent(
        ActitoNotificationContent content)
    {
        return new Binding.ActitoNotificationContent(
            type: content.Type,
            data: ToNativeExtraPrimitive(content.Data)
        );
    }

    public static Binding.ActitoNotificationAction ToNativeNotificationAction(ActitoNotificationAction action)
    {
        return new Binding.ActitoNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive ?? false,
            icon: action.Icon == null ? null : ToNativeNotificationActionIcon(action.Icon)
        );
    }

    private static Binding.ActitoNotificationActionIcon ToNativeNotificationActionIcon(
        ActitoNotificationActionIcon icon)
    {
        return new Binding.ActitoNotificationActionIcon(
            android: icon.Android,
            ios: icon.IOS,
            web: icon.Web
        );
    }

    private static Binding.ActitoNotificationAttachment ToNativeNotificationAttachment(
        ActitoNotificationAttachment attachment)
    {
        return new Binding.ActitoNotificationAttachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }

    /// <summary>
    /// Create a <see cref="Binding.ActitoDoNotDisturb"/> binding object from the <see cref="ActitoDoNotDisturb"/> data model.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static Binding.ActitoDoNotDisturb ToNativeDoNotDisturb(ActitoDoNotDisturb dnd)
    {
        return new Binding.ActitoDoNotDisturb(
            new Binding.ActitoTime(dnd.Start.Hours, dnd.Start.Minutes),
            new Binding.ActitoTime(dnd.End.Hours, dnd.End.Minutes)
        );
    }

    /// <summary>
    /// Create an "extra" dictionary consumable by the binding.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static NSDictionary<NSString, NSObject> ToNativeExtraDictionary(IDictionary<string, object> data)
    {
        if (data.Count == 0) return new NSDictionary<NSString, NSObject>();
        
        return NSDictionary<NSString, NSObject>.FromObjectsAndKeys(
            data.Values.Select(ToNativeExtraPrimitive).ToArray(),
            data.Keys.Select(key => new NSString(key)).ToArray(),
            data.Count
        );
    }

    private static NSObject ToNativeExtraPrimitive(object value)
    {
        return value switch
        {
            string s => new NSString(s),
            int i => NSNumber.FromInt32(i),
            long l => NSNumber.FromInt64(l),
            double d => NSNumber.FromDouble(d),
            float f => NSNumber.FromFloat(f),
            bool b => NSNumber.FromBoolean(b),
            IList<object> list => NSArray<NSObject>.FromNSObjects(list.Select(ToNativeExtraPrimitive).ToArray()),
            IDictionary<string, object> objects => ToNativeExtraDictionary(objects),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be represented as JSON.")
        };
    }

    #endregion
}
