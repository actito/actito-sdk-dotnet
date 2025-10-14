import CoreLocation
import Foundation
import ActitoKit
import ActitoGeoKit

@MainActor
@objc(ActitoGeoNativeBinding)
public class ActitoGeoNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.geo().delegate = self
    }

    @objc
    public weak var delegate: ActitoGeoNativeBindingDelegate?


    @objc
    public var hasLocationServicesEnabled: Bool {
        Actito.shared.geo().hasLocationServicesEnabled
    }

    @objc
    public var hasBluetoothEnabled: Bool {
        Actito.shared.geo().hasBluetoothEnabled
    }

    @objc
    public var monitoredRegions: [ActitoRegion] {
        Actito.shared.geo().monitoredRegions.map {
            ActitoRegion(from: $0)
        }
    }

    @objc
    public var enteredRegions: [ActitoRegion] {
        Actito.shared.geo().enteredRegions.map {
            ActitoRegion(from: $0)
        }
    }

    @objc
    public func enableLocationUpdates() {
        Actito.shared.geo().enableLocationUpdates()
    }

    @objc
    public func disableLocationUpdates() {
        Actito.shared.geo().disableLocationUpdates()
    }
}

extension ActitoGeoNativeBinding : ActitoGeoDelegate {
    public func actito(_ actitoGeo: ActitoGeo, didUpdateLocations locations: [ActitoGeoKit.ActitoLocation]) {
        delegate?.actito(self, didUpdateLocations: locations.map { ActitoLocation(from: $0) })
    }

    public func actito(_ actitoGeo: ActitoGeo, didFailWith error: any Error) {
        delegate?.actito(self, didFailWith: error)
    }

    public func actito(_ actitoGeo: ActitoGeo, didStartMonitoringFor region: ActitoGeoKit.ActitoRegion) {
        delegate?.actito(self, didStartMonitoringForRegion: ActitoRegion(from: region))
    }

    public func actito(_ actitoGeo: ActitoGeo, didStartMonitoringFor beacon: ActitoGeoKit.ActitoBeacon) {
        delegate?.actito(self, didStartMonitoringForBeacon: ActitoBeacon(from: beacon))
    }

    public func actito(_ actitoGeo: ActitoGeo, monitoringDidFailFor region: ActitoGeoKit.ActitoRegion, with error: any Error) {
        delegate?.actito(self, monitoringDidFailForRegion: ActitoRegion(from: region), with: error)
    }

    public func actito(_ actitoGeo: ActitoGeo, monitoringDidFailFor beacon: ActitoGeoKit.ActitoBeacon, with error: any Error) {
        delegate?.actito(self, monitoringDidFailForBeacon: ActitoBeacon(from: beacon), with: error)
    }

    public func actito(_ actitoGeo: ActitoGeo, didDetermineState state: CLRegionState, for region: ActitoGeoKit.ActitoRegion) {
        delegate?.actito(self, didDetermineState: state, forRegion: ActitoRegion(from: region))
    }

    public func actito(_ actitoGeo: ActitoGeo, didDetermineState state: CLRegionState, for beacon: ActitoGeoKit.ActitoBeacon) {
        delegate?.actito(self, didDetermineState: state, forBeacon: ActitoBeacon(from: beacon))
    }

    public func actito(_ actitoGeo: ActitoGeo, didEnter region: ActitoGeoKit.ActitoRegion) {
        delegate?.actito(self, didEnterRegion: ActitoRegion(from: region))
    }

    public func actito(_ actitoGeo: ActitoGeo, didEnter beacon: ActitoGeoKit.ActitoBeacon) {
        delegate?.actito(self, didEnterBeacon: ActitoBeacon(from: beacon))
    }

    public func actito(_ actitoGeo: ActitoGeo, didExit region: ActitoGeoKit.ActitoRegion) {
        delegate?.actito(self, didExitRegion: ActitoRegion(from: region))
    }

    public func actito(_ actitoGeo: ActitoGeo, didExit beacon: ActitoGeoKit.ActitoBeacon) {
        delegate?.actito(self, didExitBeacon: ActitoBeacon(from: beacon))
    }

    public func actito(_ actitoGeo: ActitoGeo, didVisit visit: ActitoGeoKit.ActitoVisit) {
        delegate?.actito(self, didVisit: ActitoVisit(from: visit))
    }

    public func actito(_ actitoGeo: ActitoGeo, didUpdateHeading heading: ActitoGeoKit.ActitoHeading) {
        delegate?.actito(self, didUpdateHeading: ActitoHeading(from: heading))
    }

    public func actito(_ actitoGeo: ActitoGeo, didRange beacons: [ActitoGeoKit.ActitoBeacon], in region: ActitoGeoKit.ActitoRegion) {
        delegate?.actito(self, didRange: beacons.map { ActitoBeacon(from: $0) }, in: ActitoRegion(from: region))
    }

    public func actito(_ actitoGeo: ActitoGeo, didFailRangingFor region: ActitoGeoKit.ActitoRegion, with error: any Error) {
        delegate?.actito(self, didFailRangingFor: ActitoRegion(from: region), with: error)
    }
}

@objc
public protocol ActitoGeoNativeBindingDelegate : NSObjectProtocol {
    func actito(_ actitoGeo: ActitoGeoNativeBinding, didUpdateLocations locations: [ActitoLocation])

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didFailWith error: Error)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didStartMonitoringForRegion region: ActitoRegion)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didStartMonitoringForBeacon beacon: ActitoBeacon)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, monitoringDidFailForRegion region: ActitoRegion, with error: Error)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, monitoringDidFailForBeacon beacon: ActitoBeacon, with error: Error)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didDetermineState state: CLRegionState, forRegion region: ActitoRegion)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didDetermineState state: CLRegionState, forBeacon beacon: ActitoBeacon)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didEnterRegion region: ActitoRegion)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didEnterBeacon beacon: ActitoBeacon)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didExitRegion region: ActitoRegion)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didExitBeacon beacon: ActitoBeacon)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didVisit visit: ActitoVisit)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didUpdateHeading heading: ActitoHeading)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didRange beacons: [ActitoBeacon], in region: ActitoRegion)

    func actito(_ actitoGeo: ActitoGeoNativeBinding, didFailRangingFor region: ActitoRegion, with error: Error)
}
