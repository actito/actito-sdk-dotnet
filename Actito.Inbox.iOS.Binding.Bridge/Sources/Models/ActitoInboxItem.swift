import Foundation
import ActitoInboxKit
import ActitoBinding

@objc
public class ActitoInboxItem : NSObject {
    @objc public let inboxItemId: String
    @objc public let notification: ActitoNotification
    @objc public let time: Date
    @objc public let opened: Bool
    @objc public let expires: Date?

    @objc public init(inboxItemId: String, notification: ActitoNotification, time: Date, opened: Bool, expires: Date?) {
        self.inboxItemId = inboxItemId
        self.notification = notification
        self.time = time
        self.opened = opened
        self.expires = expires
    }

    public convenience init(from item: ActitoInboxKit.ActitoInboxItem) {
        self.init(
            inboxItemId: item.id,
            notification: ActitoBinding.ActitoNotification(from: item.notification),
            time: item.time,
            opened: item.opened,
            expires: item.expires
        )
    }

    public func toNative() -> ActitoInboxKit.ActitoInboxItem {
        return ActitoInboxKit.ActitoInboxItem(
            id: inboxItemId,
            notification: notification.toNative(),
            time: time,
            opened: opened,
            expires: expires
        )
    }
}
