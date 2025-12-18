import Foundation
import ActitoKit

@objc
public class ActitoDoNotDisturb : NSObject {
    @objc public let start: ActitoTime
    @objc public let end: ActitoTime

    @objc public init(start: ActitoTime, end: ActitoTime) {
        self.start = start
        self.end = end
    }

    public convenience init(from dnd: ActitoKit.ActitoDoNotDisturb) {
        self.init(
            start: ActitoTime(from: dnd.start),
            end: ActitoTime(from: dnd.end)
        )
    }

    public func toNative() throws -> ActitoKit.ActitoDoNotDisturb {
        return ActitoKit.ActitoDoNotDisturb(
            start: try ActitoKit.ActitoTime(
                hours: start.hours,
                minutes: start.minutes
            ),
            end: try ActitoKit.ActitoTime(
                hours: end.hours,
                minutes: end.minutes
            )
        )
    }
}
