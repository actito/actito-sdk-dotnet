import Foundation
import ActitoPushKit

@objc
public class ActitoPushSubscription : NSObject {
    @objc public let token: String

    @objc public init(token: String) {
        self.token = token
    }

    public convenience init(from subscription: ActitoPushKit.ActitoPushSubscription) {
        self.init(
            token: subscription.token
        )
    }
}
