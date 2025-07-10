import Foundation
import ActitoPushKit

@objc
public class ActitoSystemNotification : NSObject {
    @objc public let id: String
    @objc public let type: String
    @objc public let extra: [String : Any]

    @objc public init(id: String, type: String, extra: [String : Any]) {
        self.id = id
        self.type = type
        self.extra = extra
    }

    convenience init(from notification: ActitoPushKit.ActitoSystemNotification) {
        self.init(
            id: notification.id,
            type: notification.type,
            extra: notification.extra
        )
    }
}
