import Foundation
import ActitoKit

@objc
public class ActitoTime : NSObject {
    @objc public let hours: Int
    @objc public let minutes: Int

    @objc public init(hours: Int, minutes: Int) {
        self.hours = hours
        self.minutes = minutes
    }

    public convenience init(from time: ActitoKit.ActitoTime) {
        self.init(
            hours: time.hours,
            minutes: time.minutes
        )
    }
}
