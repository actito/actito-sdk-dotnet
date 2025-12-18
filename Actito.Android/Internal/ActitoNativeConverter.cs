using Android.Runtime;
using ActitoSdk.Core.Models;

namespace ActitoSdk.Android.Internal;

public static class ActitoNativeConverter
{
    #region Decoding from native

    /// <summary>
    /// Create a <see cref="ActitoApplication"/> from the binding object.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static ActitoApplication FromNativeApplication(
        Binding.Models.ActitoApplication application)
    {
        var inboxConfig = application.GetInboxConfig();
        var regionConfig = application.GetRegionConfig();

        return new ActitoApplication(
            id: application.Id,
            name: application.Name,
            category: application.Category,
            appStoreId: null,
            services: application.Services.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.BooleanValue()),
            inboxConfig: inboxConfig != null ? FromNativeApplicationInboxConfig(inboxConfig) : null,
            regionConfig: regionConfig != null ? FromNativeApplicationRegionConfig(regionConfig) : null,
            userDataFields: application.UserDataFields.Select(FromNativeApplicationUserDataField).ToList(),
            actionCategories: application.ActionCategories.Select(FromNativeApplicationActionCategory).ToList()
        );
    }

    private static ActitoApplicationInboxConfig FromNativeApplicationInboxConfig(
        Binding.Models.ActitoApplication.InboxConfig inboxConfig)
    {
        return new ActitoApplicationInboxConfig(
            useInbox: inboxConfig.UseInbox,
            useUserInbox: inboxConfig.UseUserInbox,
            autoBadge: inboxConfig.AutoBadge
        );
    }

    private static ActitoApplicationRegionConfig FromNativeApplicationRegionConfig(
        Binding.Models.ActitoApplication.RegionConfig regionConfig)
    {
        return new ActitoApplicationRegionConfig(
            proximityUUID: regionConfig.ProximityUUID
        );
    }

    private static ActitoApplicationUserDataField FromNativeApplicationUserDataField(
        Binding.Models.ActitoApplication.UserDataField userDataField)
    {
        return new ActitoApplicationUserDataField(
            type: userDataField.Type,
            key: userDataField.Key,
            label: userDataField.Label
        );
    }

    private static ActitoApplicationActionCategory FromNativeApplicationActionCategory(
        Binding.Models.ActitoApplication.ActionCategory actionCategory)
    {
        return new ActitoApplicationActionCategory(
            name: actionCategory.Name,
            description: actionCategory.Description,
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
        Binding.Models.ActitoNotification notification)
    {
        return new ActitoNotification(
            partial: notification.Partial,
            id: notification.Id,
            type: notification.Type,
            time: DateTimeOffset.FromUnixTimeMilliseconds(notification.Time.Time).DateTime,
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.GetContent().Select(FromNativeNotificationContent).ToList(),
            actions: notification.Actions.Select(FromNativeNotificationAction).ToList(),
            attachments: notification.Attachments.Select(FromNativeNotificationAttachment).ToList(),
            extra: FromNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: null
        );
    }

    private static ActitoNotificationContent FromNativeNotificationContent(
        Binding.Models.ActitoNotification.Content content)
    {
        return new ActitoNotificationContent(
            type: content.Type,
            data: content.Data switch
            {
                Java.Lang.String s => s.ToString(),
                Java.Util.IMap m => new JavaDictionary<Java.Lang.String, Java.Lang.Object?>(
                        m.Handle,
                        JniHandleOwnership.DoNotRegister
                    )
                    .Where(e => e.Value != null)
                    .Cast<KeyValuePair<Java.Lang.String, Java.Lang.Object>>()
                    .ToDictionary(
                        kvp => kvp.Key.ToString(),
                        kvp => FromNativeExtraPrimitive(kvp.Value)
                    ),
                _ => new Dictionary<string, object>()
            }
        );
    }

    public static ActitoNotificationAction FromNativeNotificationAction(
        Binding.Models.ActitoNotification.Action action)
    {
        var icon = action.GetIcon();

        return new ActitoNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive?.BooleanValue(),
            icon: icon != null ? FromNativeNotificationActionIcon(icon) : null
        );
    }

    private static ActitoNotificationActionIcon FromNativeNotificationActionIcon(
        Binding.Models.ActitoNotification.Action.Icon icon)
    {
        return new ActitoNotificationActionIcon(
            android: icon.Android,
            ios: icon.Ios,
            web: icon.Web
        );
    }

    private static ActitoNotificationAttachment FromNativeNotificationAttachment(
        Binding.Models.ActitoNotification.Attachment attachment)
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
    internal static ActitoDynamicLink FromNativeDynamicLink(
        Binding.Models.ActitoDynamicLink dynamicLink)
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
    internal static ActitoDevice FromNativeDevice(Binding.Models.ActitoDevice device)
    {
        return new ActitoDevice(
            id: device.Id,
            userId: device.UserId,
            userName: device.UserName,
            timeZoneOffset: device.TimeZoneOffset,
            dnd: device.Dnd == null ? null : FromNativeDoNotDisturb(device.Dnd),
            userData: device.UserData
        );
    }

    /// <summary>
    /// Create a <see cref="ActitoDoNotDisturb"/> from the binding object.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static ActitoDoNotDisturb FromNativeDoNotDisturb(Binding.Models.ActitoDoNotDisturb dnd)
    {
        return new ActitoDoNotDisturb(
            start: new ActitoTime(
                hours: dnd.Start.Hours,
                minutes: dnd.Start.Minutes
            ),
            end: new ActitoTime(
                hours: dnd.End.Hours,
                minutes: dnd.End.Minutes
            )
        );
    }

    /// <summary>
    /// Create an "extra" dictionary from the binding representation.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static IDictionary<string, object> FromNativeExtraDictionary(IDictionary<string, Java.Lang.Object> data)
    {
        return data.ToDictionary(
            kvp => kvp.Key,
            kvp => FromNativeExtraPrimitive(kvp.Value)
        );
    }

    private static object FromNativeExtraPrimitive(Java.Lang.Object value)
    {
        return value switch
        {
            Java.Lang.String s => s.ToString(),
            Java.Lang.Integer i => i.IntValue(),
            Java.Lang.Long l => l.LongValue(),
            Java.Lang.Double d => d.DoubleValue(),
            Java.Lang.Float f => f.FloatValue(),
            Java.Lang.Boolean b => b.BooleanValue(),
            JavaList l => l.Cast<object>()
                .Select(item => item is Java.Lang.Object o ? FromNativeExtraPrimitive(o) : item)
                .ToList(),
            JavaDictionary d => new JavaDictionary<string, Java.Lang.Object>(d.Handle, JniHandleOwnership.DoNotRegister)
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => FromNativeExtraPrimitive(kvp.Value)
                ),
            Java.Util.IMap m => new JavaDictionary<string, Java.Lang.Object>(m.Handle, JniHandleOwnership.DoNotRegister)
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => FromNativeExtraPrimitive(kvp.Value)
                ),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be decoded.")
        };
    }

    #endregion

    #region Enconding to native

    public static Binding.Models.ActitoNotification ToNativeNotification(ActitoNotification notification)
    {
        return new Binding.Models.ActitoNotification(
            partial: notification.Partial,
            id: notification.Id,
            type: notification.Type,
            time: new Java.Util.Date(new DateTimeOffset(notification.Time).ToUnixTimeMilliseconds()),
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(ToNativeNotificationContent).ToArray(),
            actions: notification.Actions.Select(ToNativeNotificationAction).ToArray(),
            attachments: notification.Attachments.Select(ToNativeNotificationAttachment).ToArray(),
            extra: ToNativeExtraDictionary(notification.Extra)
        );
    }

    private static Binding.Models.ActitoNotification.Content ToNativeNotificationContent(
        ActitoNotificationContent content)
    {
        return new Binding.Models.ActitoNotification.Content(
            type: content.Type,
            data: ToNativeExtraPrimitive(content.Data)
        );
    }

    public static Binding.Models.ActitoNotification.Action ToNativeNotificationAction(
        ActitoNotificationAction action)
    {
        return new Binding.Models.ActitoNotification.Action(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive == null ? null : Java.Lang.Boolean.ValueOf((bool)action.Destructive),
            icon: action.Icon == null ? null : ToNativeNotificationActionIcon(action.Icon)
        );
    }

    private static Binding.Models.ActitoNotification.Action.Icon ToNativeNotificationActionIcon(
        ActitoNotificationActionIcon icon)
    {
        return new Binding.Models.ActitoNotification.Action.Icon(
            android: icon.Android,
            ios: icon.IOS,
            web: icon.Web
        );
    }

    private static Binding.Models.ActitoNotification.Attachment ToNativeNotificationAttachment(
        ActitoNotificationAttachment attachment)
    {
        return new Binding.Models.ActitoNotification.Attachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }

    /// <summary>
    /// Create a <see cref="Binding.Models.ActitoDoNotDisturb"/> binding object from the <see cref="ActitoDoNotDisturb"/> data model.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static Binding.Models.ActitoDoNotDisturb ToNativeDoNotDisturb(ActitoDoNotDisturb dnd)
    {
        return new Binding.Models.ActitoDoNotDisturb(
            new Binding.Models.ActitoTime(dnd.Start.Hours, dnd.Start.Minutes),
            new Binding.Models.ActitoTime(dnd.End.Hours, dnd.End.Minutes)
        );
    }

    /// <summary>
    /// Create an "extra" dictionary consumable by the binding. 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static IDictionary<string, Java.Lang.Object> ToNativeExtraDictionary(IDictionary<string, object> data)
    {
        return data.ToDictionary(
            kvp => kvp.Key,
            kvp => ToNativeExtraPrimitive(kvp.Value)
        );
    }

    private static Java.Lang.Object ToNativeExtraPrimitive(object value)
    {
        return value switch
        {
            string s => new Java.Lang.String(s),
            int i => Java.Lang.Integer.ValueOf(i),
            long l => Java.Lang.Long.ValueOf(l),
            float f => Java.Lang.Float.ValueOf(f),
            double d => Java.Lang.Double.ValueOf(d),
            bool b => Java.Lang.Boolean.ValueOf(b),
            IList<object> list => new Java.Util.ArrayList(
                list.Select(ToNativeExtraPrimitive).ToList()
            ),
            IDictionary<string, object> objects => new Java.Util.HashMap(
                objects.ToDictionary(
                    kvp => kvp.Key,
                    kvp => ToNativeExtraPrimitive(kvp.Value)
                )
            ),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be represented as JSON.")
        };
    }

    #endregion
}
