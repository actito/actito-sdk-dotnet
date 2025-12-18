import Foundation
import ActitoGeoKit

@objc
public class ActitoLocation : NSObject {
    @objc public let latitude: Double
    @objc public let longitude: Double
    @objc public let altitude: Double
    @objc public let course: Double
    @objc public let speed: Double
    @objc public let floor: NSNumber? // Cannot represent Int? in Objective-C.
    @objc public let horizontalAccuracy: Double
    @objc public let verticalAccuracy: Double
    @objc public let timestamp: Date

    @objc public init(latitude: Double, longitude: Double, altitude: Double, course: Double, speed: Double, floor: NSNumber?, horizontalAccuracy: Double, verticalAccuracy: Double, timestamp: Date) {
        self.latitude = latitude
        self.longitude = longitude
        self.altitude = altitude
        self.course = course
        self.speed = speed
        self.floor = floor
        self.horizontalAccuracy = horizontalAccuracy
        self.verticalAccuracy = verticalAccuracy
        self.timestamp = timestamp
    }

    public convenience init(from location: ActitoGeoKit.ActitoLocation) {
        self.init(
            latitude: location.latitude,
            longitude: location.longitude,
            altitude: location.altitude,
            course: location.course,
            speed: location.speed,
            floor: location.floor.map { NSNumber(integerLiteral: $0) },
            horizontalAccuracy: location.horizontalAccuracy,
            verticalAccuracy: location.verticalAccuracy,
            timestamp: location.timestamp
        )
    }
}
