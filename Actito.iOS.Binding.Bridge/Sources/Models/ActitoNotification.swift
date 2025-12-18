import Foundation
import ActitoKit

@objc
public class ActitoNotification : NSObject {
    @objc public let partial: Bool
    @objc public let notificationId: String
    @objc public let type: String
    @objc public let time: Date
    @objc public let title: String?
    @objc public let subtitle: String?
    @objc public let message: String
    @objc public let content: [ActitoNotificationContent]
    @objc public let actions: [ActitoNotificationAction]
    @objc public let attachments: [ActitoNotificationAttachment]
    @objc public let extra: [String : Any]
    @objc public let targetContentIdentifier: String?

    @objc public init(partial: Bool, notificationId: String, type: String, time: Date, title: String?, subtitle: String?, message: String, content: [ActitoNotificationContent], actions: [ActitoNotificationAction], attachments: [ActitoNotificationAttachment], extra: [String : Any], targetContentIdentifier: String?) {
        self.partial = partial
        self.notificationId = notificationId
        self.type = type
        self.time = time
        self.title = title
        self.subtitle = subtitle
        self.message = message
        self.content = content
        self.actions = actions
        self.attachments = attachments
        self.extra = extra
        self.targetContentIdentifier = targetContentIdentifier
    }

    public convenience init(from notification: ActitoKit.ActitoNotification) {
        self.init(
            partial: notification.partial,
            notificationId: notification.id,
            type: notification.type,
            time: notification.time,
            title: notification.title,
            subtitle: notification.subtitle,
            message: notification.message,
            content: notification.content.map { ActitoNotificationContent(from: $0) },
            actions: notification.actions.map { ActitoNotificationAction(from: $0) },
            attachments: notification.attachments.map { ActitoNotificationAttachment(from: $0) },
            extra: notification.extra,
            targetContentIdentifier: notification.targetContentIdentifier
        )
    }

    public func toNative() -> ActitoKit.ActitoNotification {
        return ActitoKit.ActitoNotification(
            partial: partial,
            id: notificationId,
            type: type,
            time: time,
            title: title,
            subtitle: subtitle,
            message: message,
            content: content.map { $0.toNative() },
            actions: actions.map { $0.toNative() },
            attachments: attachments.map { $0.toNative() },
            extra: extra,
            targetContentIdentifier: targetContentIdentifier
        )
    }
}

@objc
public class ActitoNotificationContent : NSObject {
    @objc public let type: String
    @objc public let data: Any

    @objc public init(type: String, data: Any) {
        self.type = type
        self.data = data
    }

    public convenience init(from content: ActitoKit.ActitoNotification.Content) {
        self.init(
            type: content.type,
            data: content.data
        )
    }

    public func toNative() -> ActitoKit.ActitoNotification.Content {
        return ActitoKit.ActitoNotification.Content(
            type: type,
            data: data
        )
    }
}

@objc
public class ActitoNotificationAction : NSObject {
    @objc public let type: String
    @objc public let label: String
    @objc public let target: String?
    @objc public let keyboard: Bool
    @objc public let camera: Bool
    @objc public let destructive: Bool
    @objc public let icon: ActitoNotificationActionIcon?

    @objc public init(type: String, label: String, target: String?, keyboard: Bool, camera: Bool, destructive: Bool, icon: ActitoNotificationActionIcon?) {
        self.type = type
        self.label = label
        self.target = target
        self.keyboard = keyboard
        self.camera = camera
        self.destructive = destructive
        self.icon = icon
    }

    public convenience init(from action: ActitoKit.ActitoNotification.Action) {
        self.init(
            type: action.type,
            label: action.label,
            target: action.target,
            keyboard: action.keyboard,
            camera: action.camera,
            destructive: action.destructive ?? false,
            icon: action.icon.map { ActitoNotificationActionIcon(from: $0) }
        )
    }

    public func toNative() -> ActitoKit.ActitoNotification.Action {
        return ActitoKit.ActitoNotification.Action(
            type: type,
            label: label,
            target: target,
            keyboard: keyboard,
            camera: camera,
            destructive: destructive,
            icon: icon.map { $0.toNative() }
        )
    }
}

@objc
public class ActitoNotificationActionIcon : NSObject {
    @objc public let android: String?
    @objc public let ios: String?
    @objc public let web: String?

    @objc public init(android: String?, ios: String?, web: String?) {
        self.android = android
        self.ios = ios
        self.web = web
    }

    public convenience init(from icon: ActitoKit.ActitoNotification.Action.Icon) {
        self.init(
            android: icon.android,
            ios: icon.ios,
            web: icon.web
        )
    }

    public func toNative() -> ActitoKit.ActitoNotification.Action.Icon {
        return ActitoKit.ActitoNotification.Action.Icon(
            android: android,
            ios: ios,
            web: web
        )
    }
}

@objc
public class ActitoNotificationAttachment : NSObject {
    @objc public let mimeType: String
    @objc public let uri: String

    @objc public init(mimeType: String, uri: String) {
        self.mimeType = mimeType
        self.uri = uri
    }

    public convenience init(from attachment: ActitoKit.ActitoNotification.Attachment) {
        self.init(
            mimeType: attachment.mimeType,
            uri: attachment.uri
        )
    }

    public func toNative() -> ActitoKit.ActitoNotification.Attachment {
        return ActitoKit.ActitoNotification.Attachment(
            mimeType: mimeType,
            uri: uri
        )
    }
}
