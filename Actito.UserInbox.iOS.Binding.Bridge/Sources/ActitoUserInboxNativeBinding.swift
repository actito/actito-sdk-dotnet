import Foundation
import ActitoKit
import ActitoUserInboxKit
import ActitoBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@MainActor
@objc(ActitoUserInboxNativeBinding)
public class ActitoUserInboxNativeBinding : NSObject {

    @objc
    public func parseResponseFromString(_ str: String, _ onSuccess: @escaping SuccessBlock<ActitoUserInboxResponse>, _ onFailure: @escaping ErrorBlock) {
        do {
            let response = try Actito.shared.userInbox().parseResponse(string: str)
            onSuccess(ActitoUserInboxResponse(from: response))
        } catch {
            onFailure(error)
        }
    }

    @objc
    public func open(_ item: ActitoUserInboxItem, _ onSuccess: @escaping SuccessBlock<ActitoBinding.ActitoNotification>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.userInbox().open(item.toNative()) { result in
            switch result {
            case let .success(notification):
                onSuccess(ActitoNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAsRead(_ item: ActitoUserInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.userInbox().markAsRead(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func remove(_ item: ActitoUserInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.userInbox().remove(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
