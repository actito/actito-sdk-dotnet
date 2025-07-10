import Foundation
import ActitoPushKit

@objc
public enum ActitoTransport: Int {
    case notificare
    case apns
    case unknown

    public init(from transport: ActitoPushKit.ActitoTransport?) {
        switch transport {
        case .notificare:
            self = .notificare
        case .apns:
            self = .apns
        case .none:
            self = .unknown
        @unknown default:
            self = .unknown
        }
    }
}
