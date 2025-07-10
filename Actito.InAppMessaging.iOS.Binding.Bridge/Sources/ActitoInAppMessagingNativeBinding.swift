import Foundation
import ActitoKit
import ActitoInAppMessagingKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoInAppMessagingNativeBinding)
public class ActitoInAppMessagingNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.inAppMessaging().delegate = self
    }

    @objc
    public weak var delegate: ActitoInAppMessagingNativeBindingDelegate?

    @objc
    public var hasMessagesSuppressed: Bool {
        get { Actito.shared.inAppMessaging().hasMessagesSuppressed }
        set { Actito.shared.inAppMessaging().hasMessagesSuppressed = newValue }
    }

    @objc public func setMessagesSuppressed(_ suppressed: Bool, evaluateContext: Bool) {
        Actito.shared.inAppMessaging().setMessagesSuppressed(suppressed, evaluateContext: evaluateContext)
    }
}

extension ActitoInAppMessagingNativeBinding : ActitoInAppMessagingDelegate {
    public func actito(_ actito: any ActitoInAppMessaging, didPresentMessage message: ActitoInAppMessagingKit.ActitoInAppMessage) {
        delegate?.actito(self, didPresentMessage: ActitoInAppMessage(from: message))
    }

    public func actito(_ actito: any ActitoInAppMessaging, didFinishPresentingMessage message: ActitoInAppMessagingKit.ActitoInAppMessage) {
        delegate?.actito(self, didFinishPresentingMessage: ActitoInAppMessage(from: message))
    }

    public func actito(_ actito: any ActitoInAppMessaging, didFailToPresentMessage message: ActitoInAppMessagingKit.ActitoInAppMessage) {
        delegate?.actito(self, didFailToPresentMessage: ActitoInAppMessage(from: message))
    }

    public func actito(_ actito: any ActitoInAppMessaging, didExecuteAction action: ActitoInAppMessagingKit.ActitoInAppMessage.Action, for message: ActitoInAppMessagingKit.ActitoInAppMessage) {
        delegate?.actito(self, didExecuteAction: ActitoInAppMessageAction(from: action), for: ActitoInAppMessage(from: message))
    }

    public func actito(_ actito: any ActitoInAppMessaging, didFailToExecuteAction action: ActitoInAppMessagingKit.ActitoInAppMessage.Action, for message: ActitoInAppMessagingKit.ActitoInAppMessage, error: (any Error)?) {
        delegate?.actito(self, didFailToExecuteAction: ActitoInAppMessageAction(from: action), for: ActitoInAppMessage(from: message), error: error)
    }
}

@objc
public protocol ActitoInAppMessagingNativeBindingDelegate : NSObjectProtocol {
    func actito(_ actito: ActitoInAppMessagingNativeBinding, didPresentMessage message: ActitoInAppMessage)

    func actito(_ actito: ActitoInAppMessagingNativeBinding, didFinishPresentingMessage message: ActitoInAppMessage)

    func actito(_ actito: ActitoInAppMessagingNativeBinding, didFailToPresentMessage message: ActitoInAppMessage)

    func actito(_ actito: ActitoInAppMessagingNativeBinding, didExecuteAction action: ActitoInAppMessageAction, for message: ActitoInAppMessage)

    func actito(_ actito: ActitoInAppMessagingNativeBinding, didFailToExecuteAction action: ActitoInAppMessageAction, for message: ActitoInAppMessage, error: Error?)
}
