import Foundation
import ActitoGeoKit

@objc
public class ActitoBeacon : NSObject {
    @objc public let beaconId: String
    @objc public let name: String
    @objc public let major: Int
    @objc public let minor: NSNumber? // Cannot represent Int? in Objective-C.
    @objc public let triggers: Bool
    @objc public let proximity: ActitoBeaconProximity

    @objc public init(beaconId: String, name: String, major: Int, minor: NSNumber?, triggers: Bool, proximity: ActitoBeaconProximity) {
        self.beaconId = beaconId
        self.name = name
        self.major = major
        self.minor = minor
        self.triggers = triggers
        self.proximity = proximity
    }

    public convenience init(from beacon: ActitoGeoKit.ActitoBeacon) {
        self.init(
            beaconId: beacon.id,
            name: beacon.name,
            major: beacon.major,
            minor: beacon.minor.map { NSNumber(integerLiteral: $0) },
            triggers: beacon.triggers,
            proximity: ActitoBeaconProximity(from: beacon.proximity)
        )
    }
}

@objc
public enum ActitoBeaconProximity : Int {
    case unknown
    case immediate
    case near
    case far

    init(from proximity: ActitoGeoKit.ActitoBeacon.Proximity) {
        switch proximity {
        case .unknown:
            self = .unknown
        case .immediate:
            self = .immediate
        case .near:
            self = .near
        case .far:
            self = .far
        @unknown default:
            fatalError("Unknown beacon proximity: \(proximity.rawValue)")
        }
    }
}
