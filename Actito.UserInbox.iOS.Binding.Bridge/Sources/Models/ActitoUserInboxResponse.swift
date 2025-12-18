import Foundation
import ActitoUserInboxKit

@objc
public class ActitoUserInboxResponse : NSObject {
    @objc public let count: Int
    @objc public let unread: Int
    @objc public let items: [ActitoUserInboxItem]

    @objc public init(count: Int, unread: Int, items: [ActitoUserInboxItem]) {
        self.count = count
        self.unread = unread
        self.items = items
    }

    public convenience init(from response: ActitoUserInboxKit.ActitoUserInboxResponse) {
        self.init(
            count: response.count,
            unread: response.unread,
            items: response.items.map { ActitoUserInboxItem(from: $0) }
        )
    }
}
