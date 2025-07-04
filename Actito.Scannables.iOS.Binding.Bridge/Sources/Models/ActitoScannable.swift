import Foundation
import ActitoScannablesKit
import ActitoBinding

@objc
public class ActitoScannable : NSObject {
    @objc public let scannableId: String
    @objc public let name: String
    @objc public let tag: String
    @objc public let type: String
    @objc public let notification: ActitoNotification?

    @objc public init(scannableId: String, name: String, tag: String, type: String, notification: ActitoNotification?) {
        self.scannableId = scannableId
        self.name = name
        self.tag = tag
        self.type = type
        self.notification = notification
    }

    public convenience init(from scannable: ActitoScannablesKit.ActitoScannable) {
        self.init(
            scannableId: scannable.id,
            name: scannable.name,
            tag: scannable.tag,
            type: scannable.type,
            notification: scannable.notification.map { ActitoNotification(from: $0) }
        )
    }
}
