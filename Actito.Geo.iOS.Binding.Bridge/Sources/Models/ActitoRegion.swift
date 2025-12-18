import Foundation
import ActitoGeoKit

@objc
public class ActitoRegion : NSObject {
    @objc public let regionId: String
    @objc public let name: String
    @objc public let regionDescription: String?
    @objc public let referenceKey: String?
    @objc public let geometry: ActitoRegionGeometry
    @objc public let advancedGeometry: ActitoRegionAdvancedGeometry?
    @objc public let major: NSNumber? // Cannot represent Int? in Objective-C.
    @objc public let distance: Double
    @objc public let timeZone: String
    @objc public let timeZoneOffset: Double

    @objc public init(regionId: String, name: String, regionDescription: String?, referenceKey: String?, geometry: ActitoRegionGeometry, advancedGeometry: ActitoRegionAdvancedGeometry?, major: NSNumber?, distance: Double, timeZone: String, timeZoneOffset: Double) {
        self.regionId = regionId
        self.name = name
        self.regionDescription = regionDescription
        self.referenceKey = referenceKey
        self.geometry = geometry
        self.advancedGeometry = advancedGeometry
        self.major = major
        self.distance = distance
        self.timeZone = timeZone
        self.timeZoneOffset = timeZoneOffset
    }

    public convenience init(from region: ActitoGeoKit.ActitoRegion) {
        self.init(
            regionId: region.id,
            name: region.name,
            regionDescription: region.description,
            referenceKey: region.referenceKey,
            geometry: ActitoRegionGeometry(from: region.geometry),
            advancedGeometry: region.advancedGeometry.map { ActitoRegionAdvancedGeometry(from: $0) },
            major: region.major.map { NSNumber(integerLiteral: $0) },
            distance: region.distance,
            timeZone: region.timeZone,
            timeZoneOffset: Double(region.timeZoneOffset)
        )
    }
}

@objc
public class ActitoRegionGeometry : NSObject {
    @objc public let type: String
    @objc public let coordinate: ActitoRegionCoordinate

    @objc public init(type: String, coordinate: ActitoRegionCoordinate) {
        self.type = type
        self.coordinate = coordinate
    }

    public convenience init(from geometry: ActitoGeoKit.ActitoRegion.Geometry) {
        self.init(
            type: geometry.type,
            coordinate: ActitoRegionCoordinate(from: geometry.coordinate)
        )
    }
}

@objc
public class ActitoRegionAdvancedGeometry : NSObject {
    @objc public let type: String
    @objc public let coordinates: [ActitoRegionCoordinate]

    @objc public init(type: String, coordinates: [ActitoRegionCoordinate]) {
        self.type = type
        self.coordinates = coordinates
    }

    public convenience init(from advancedGeometry: ActitoGeoKit.ActitoRegion.AdvancedGeometry) {
        self.init(
            type: advancedGeometry.type,
            coordinates: advancedGeometry.coordinates.map { ActitoRegionCoordinate(from: $0) }
        )
    }
}

@objc
public class ActitoRegionCoordinate : NSObject {
    @objc public let latitude: Double
    @objc public let longitude: Double

    @objc public init(latitude: Double, longitude: Double) {
        self.latitude = latitude
        self.longitude = longitude
    }

    public convenience init(from coordinate: ActitoGeoKit.ActitoRegion.Coordinate) {
        self.init(
            latitude: coordinate.latitude,
            longitude: coordinate.longitude
        )
    }
}
