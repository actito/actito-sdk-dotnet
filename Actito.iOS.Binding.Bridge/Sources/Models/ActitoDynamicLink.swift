import Foundation
import ActitoKit

@objc
public class ActitoDynamicLink : NSObject {
    @objc public let target: String

    @objc public init(target: String) {
        self.target = target
    }

    public convenience init(from link: ActitoKit.ActitoDynamicLink) {
        self.init(target: link.target)
    }
}
