import Foundation
import ActitoPushKit

@objc
public enum ActitoNotificationDeliveryMechanism : Int {
    case standard
    case silent

    // Objective-Sharpie uses the Objective-C, but removes the common prefix in enum cases.
    // Without this case, 'ActitoNotificationDeliveryMechanismStandard' would become 'tandard'.
    case unknown

    init(from deliveryMechanism: ActitoPushKit.ActitoNotificationDeliveryMechanism) {
        switch deliveryMechanism {
        case .standard:
            self = .standard
        case .silent:
            self = .silent
        @unknown default:
            fatalError("Unknown delivery mechanism: \(deliveryMechanism.rawValue)")
        }
    }
}
