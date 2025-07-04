import Foundation
import ActitoGeoKit

@objc
public class ActitoHeading : NSObject {
    @objc public let magneticHeading: Double
    @objc public let trueHeading: Double
    @objc public let headingAccuracy: Double
    @objc public let x: Double
    @objc public let y: Double
    @objc public let z: Double
    @objc public let timestamp: Date

    @objc public init(magneticHeading: Double, trueHeading: Double, headingAccuracy: Double, x: Double, y: Double, z: Double, timestamp: Date) {
        self.magneticHeading = magneticHeading
        self.trueHeading = trueHeading
        self.headingAccuracy = headingAccuracy
        self.x = x
        self.y = y
        self.z = z
        self.timestamp = timestamp
    }

    public convenience init(from heading: ActitoGeoKit.ActitoHeading) {
        self.init(
            magneticHeading: heading.magneticHeading,
            trueHeading: heading.trueHeading,
            headingAccuracy: heading.headingAccuracy,
            x: heading.x,
            y: heading.y,
            z: heading.z,
            timestamp: heading.timestamp
        )
    }
}
