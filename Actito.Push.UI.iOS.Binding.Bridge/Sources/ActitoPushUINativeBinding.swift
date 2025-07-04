import Foundation
import UIKit
import ActitoKit
import ActitoPushUIKit
import ActitoBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoPushUINativeBinding)
public class ActitoPushUINativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.pushUI().delegate = self
    }

    @objc
    public weak var delegate: ActitoPushUINativeBindingDelegate?

    @objc
    public func presentNotification(_ notification: ActitoBinding.ActitoNotification, in controller: UIViewController) {
        Actito.shared.pushUI().presentNotification(notification.toNative(), in: controller)
    }

    @objc
    public func presentAction(_ action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification, in controller: UIViewController) {
        Actito.shared.pushUI().presentAction(action.toNative(), for: notification.toNative(), in: controller)
    }

    @objc
    public func requiresViewController(_ notification: ActitoBinding.ActitoNotification) -> Bool {
        return notification.toNative().requiresViewController
    }
}

extension ActitoPushUINativeBinding : ActitoPushUIDelegate {
    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, willPresentNotification notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, willPresentNotification: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didPresentNotification notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didPresentNotification: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didFinishPresentingNotification notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didFinishPresentingNotification: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didFailToPresentNotification notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didFailToPresentNotification: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didClickURL url: URL, in notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didClickURL: url, in: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, willExecuteAction action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, willExecuteAction: ActitoNotificationAction(from: action), for: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didExecuteAction action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didExecuteAction: ActitoNotificationAction(from: action), for: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didNotExecuteAction action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didNotExecuteAction: ActitoNotificationAction(from: action), for: ActitoNotification(from: notification))
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didFailToExecuteAction action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification, error: (any Error)?) {
        delegate?.actito(self, didFailToExecuteAction: ActitoNotificationAction(from: action), for: ActitoNotification(from: notification), error: error)
    }

    public func actito(_ actitoPushUI: any ActitoPushUIKit.ActitoPushUI, didReceiveCustomAction url: URL, in action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didReceiveCustomAction: url, in: ActitoNotificationAction(from: action), for: ActitoNotification(from: notification))
    }
}

@objc
public protocol ActitoPushUINativeBindingDelegate : NSObjectProtocol {
    func actito(_ actitoPushUI: ActitoPushUINativeBinding, willPresentNotification notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didPresentNotification notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didFinishPresentingNotification notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didFailToPresentNotification notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didClickURL url: URL, in notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, willExecuteAction action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didExecuteAction action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didNotExecuteAction action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didFailToExecuteAction action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification, error: Error?)

    func actito(_ actitoPushUI: ActitoPushUINativeBinding, didReceiveCustomAction url: URL, in action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification)
}
