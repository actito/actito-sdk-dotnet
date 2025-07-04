import Foundation
import UIKit
import UserNotifications
import ActitoKit
import ActitoPushKit
import ActitoBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoPushNativeBinding)
public class ActitoPushNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.push().delegate = self
    }

    @objc
    public var authorizationOptions: UNAuthorizationOptions {
        get {
            Actito.shared.push().authorizationOptions
        }
        set {
            Actito.shared.push().authorizationOptions = newValue
        }
    }

    @objc
    public var categoryOptions: UNNotificationCategoryOptions {
        get {
            Actito.shared.push().categoryOptions
        }
        set {
            Actito.shared.push().categoryOptions = newValue
        }
    }

    @objc
    public var presentationOptions: UNNotificationPresentationOptions {
        get {
            Actito.shared.push().presentationOptions
        }
        set {
            Actito.shared.push().presentationOptions = newValue
        }
    }

    @objc
    public var hasRemoteNotificationsEnabled: Bool {
        Actito.shared.push().hasRemoteNotificationsEnabled
    }

    @objc
    public var transport: ActitoTransport {
        ActitoTransport(from: Actito.shared.push().transport)
    }

    @objc
    public var subscription: ActitoPushSubscription? {
        Actito.shared.push().subscription.map { ActitoPushSubscription(from: $0) }
    }

    @objc
    public var allowedUI: Bool {
        Actito.shared.push().allowedUI
    }

    @objc
    public weak var delegate: ActitoPushNativeBindingDelegate?

    @objc
    public func enableRemoteNotifications(_ onSuccess: @escaping SuccessBlock<Bool>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.push().enableRemoteNotifications { result in
            switch result {
            case let .success(granted):
                onSuccess(granted)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func disableRemoteNotifications(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.push().disableRemoteNotifications { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func registeredForRemoteNotifications(_ application: UIApplication, _ token: Data) {
        Actito.shared.push().application(application, didRegisterForRemoteNotificationsWithDeviceToken: token)
    }

    @objc
    public func failedToRegisterForRemoteNotifications(_ application: UIApplication, _ error: Error) {
        Actito.shared.push().application(application, didFailToRegisterForRemoteNotificationsWithError: error)
    }

    @objc
    public func didReceiveRemoteNotification(_ application: UIApplication, _ userInfo: [AnyHashable: Any], _ completionHandler: @escaping (UIBackgroundFetchResult) -> Void) {
        Actito.shared.push().application(application, didReceiveRemoteNotification: userInfo, fetchCompletionHandler: completionHandler)
    }

    @objc
    public func willPresentNotification(_ center: UNUserNotificationCenter, _ notification: UNNotification, _ completionHandler: @escaping (UNNotificationPresentationOptions) -> Void) {
        Actito.shared.push().userNotificationCenter(center, willPresent: notification, withCompletionHandler: completionHandler)
    }

    @objc
    public func didReceiveNotificationResponse(_ center: UNUserNotificationCenter, _ response: UNNotificationResponse, _ completionHandler: @escaping () -> Void) {
        Actito.shared.push().userNotificationCenter(center, didReceive: response, withCompletionHandler: completionHandler)
    }

    @objc
    public func openSettings(_ center: UNUserNotificationCenter, _ notification: UNNotification?) {
        Actito.shared.push().userNotificationCenter(center, openSettingsFor: notification)
    }
}

extension ActitoPushNativeBinding : ActitoPushDelegate {
    public func actito(_ actitoPush: any ActitoPush, didChangeSubscription subscription: ActitoPushKit.ActitoPushSubscription?) {
        delegate?.actito(self, didChangeSubscription: subscription.map { ActitoPushSubscription.init(from: $0) })
    }

    public func actito(_ actitoPush: any ActitoPush, didChangeNotificationSettings allowedUI: Bool) {
        delegate?.actito(self, didChangeNotificationSettings: allowedUI)
    }

    public func actito(_ actitoPush: any ActitoPush, didReceiveNotification notification: ActitoKit.ActitoNotification, deliveryMechanism: ActitoPushKit.ActitoNotificationDeliveryMechanism) {
        delegate?.actito(self, didReceiveNotification: ActitoBinding.ActitoNotification(from: notification), deliveryMechanism: ActitoNotificationDeliveryMechanism(from: deliveryMechanism))
    }

    public func actito(_ actitoPush: any ActitoPush, didReceiveSystemNotification notification: ActitoPushKit.ActitoSystemNotification) {
        delegate?.actito(self, didReceiveSystemNotification: ActitoSystemNotification(from: notification))
    }

    public func actito(_ actitoPush: any ActitoPush, didReceiveUnknownNotification userInfo: [AnyHashable : Any]) {
        delegate?.actito(self, didReceiveUnknownNotification: userInfo)
    }

    public func actito(_ actitoPush: any ActitoPush, shouldOpenSettings notification: ActitoKit.ActitoNotification?) {
        delegate?.actito(self, shouldOpenSettings: notification.map { ActitoNotification(from: $0) })
    }

    public func actito(_ actitoPush: any ActitoPush, didOpenNotification notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didOpenNotification: ActitoBinding.ActitoNotification(from: notification))
    }

    public func actito(_ actitoPush: any ActitoPush, didOpenUnknownNotification userInfo: [AnyHashable : Any]) {
        delegate?.actito(self, didOpenUnknownNotification: userInfo)
    }

    public func actito(_ actitoPush: any ActitoPush, didOpenAction action: ActitoKit.ActitoNotification.Action, for notification: ActitoKit.ActitoNotification) {
        delegate?.actito(self, didOpenAction: ActitoBinding.ActitoNotificationAction(from: action), for: ActitoBinding.ActitoNotification(from: notification))
    }

    public func actito(_ actitoPush: any ActitoPush, didOpenUnknownAction action: String, for notification: [AnyHashable : Any], responseText: String?) {
        delegate?.actito(self, didOpenUnknownAction: action, for: notification, responseText: responseText)
    }
}

@objc
public protocol ActitoPushNativeBindingDelegate : NSObjectProtocol {
    // func actito(_ actitoPush: ActitoPushNativeBinding, didFailToRegisterForRemoteNotificationsWithError error: any Error)

    func actito(_ actitoPush: ActitoPushNativeBinding, didChangeSubscription subscription: ActitoPushSubscription?)

    func actito(_ actitoPush: ActitoPushNativeBinding, didChangeNotificationSettings allowedUI: Bool)

    func actito(_ actitoPush: ActitoPushNativeBinding, didReceiveUnknownNotification userInfo: [AnyHashable : Any])

    func actito(_ actitoPush: ActitoPushNativeBinding, didReceiveNotification notification: ActitoBinding.ActitoNotification, deliveryMechanism: ActitoNotificationDeliveryMechanism)

    func actito(_ actitoPush: ActitoPushNativeBinding, didReceiveSystemNotification notification: ActitoSystemNotification)

    func actito(_ actitoPush: ActitoPushNativeBinding, shouldOpenSettings notification: ActitoBinding.ActitoNotification?)

    func actito(_ actitoPush: ActitoPushNativeBinding, didOpenNotification notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPush: ActitoPushNativeBinding, didOpenUnknownNotification userInfo: [AnyHashable : Any])

    func actito(_ actitoPush: ActitoPushNativeBinding, didOpenAction action: ActitoBinding.ActitoNotificationAction, for notification: ActitoBinding.ActitoNotification)

    func actito(_ actitoPush: ActitoPushNativeBinding, didOpenUnknownAction action: String, for notification: [AnyHashable : Any], responseText: String?)
}
