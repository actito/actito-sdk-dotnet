import Foundation
import UserNotifications
import ActitoKit
import ActitoInboxKit
import ActitoBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@MainActor
@objc(ActitoInboxNativeBinding)
public class ActitoInboxNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.inbox().delegate = self
    }

    @objc
    public var items: [ActitoInboxItem] {
        Actito.shared.inbox().items.map { ActitoInboxItem(from: $0) }
    }

    @objc
    public var badge: Int {
        Actito.shared.inbox().badge
    }

    @objc
    public weak var delegate: ActitoInboxNativeBindingDelegate?

    @objc
    public func refresh(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().refresh { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func refreshBadge(_ onSuccess: @escaping SuccessBlock<Int>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().refreshBadge { result in
            switch result {
            case let .success(badge):
                onSuccess(badge)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func open(_ item: ActitoInboxItem, _ onSuccess: @escaping SuccessBlock<ActitoBinding.ActitoNotification>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().open(item.toNative()) { result in
            switch result {
            case let .success(notification):
                onSuccess(ActitoNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAsRead(_ item: ActitoInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().markAsRead(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAllAsRead(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().markAllAsRead { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func remove(_ item: ActitoInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().remove(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func clear(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.inbox().clear { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension ActitoInboxNativeBinding : ActitoInboxDelegate {
    public func actito(_ actitoInbox: ActitoInboxKit.ActitoInbox, didUpdateInbox items: [ActitoInboxKit.ActitoInboxItem]) {
        delegate?.actito(self, didUpdateInbox: items.map { ActitoInboxItem(from: $0) })
    }

    public func actito(_ actitoInbox: ActitoInboxKit.ActitoInbox, didUpdateBadge badge: Int) {
        delegate?.actito(self, didUpdateBadge: badge)
    }
}

@objc
public protocol ActitoInboxNativeBindingDelegate : NSObjectProtocol {
    func actito(_ actitoInbox: ActitoInboxNativeBinding, didUpdateInbox items: [ActitoInboxItem])

    func actito(_ actitoInbox: ActitoInboxNativeBinding, didUpdateBadge badge: Int)
}
