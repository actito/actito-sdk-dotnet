import Foundation
import ActitoNotificationServiceExtensionKit
import UserNotifications

public typealias SuccessBlock<T> = (T) -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoNotificationServiceExtensionNativeBinding)
public class ActitoNotificationServiceExtensionNativeBinding : NSObject {

    @objc
    public static func handleNotificationRequest(_ request: UNNotificationRequest, _ onSuccess: @escaping SuccessBlock<UNNotificationContent>, _ onFailure: @escaping ErrorBlock) {
        ActitoNotificationServiceExtension.handleNotificationRequest(request) { result in
            switch result {
            case let .success(content):
                onSuccess(content)

            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
