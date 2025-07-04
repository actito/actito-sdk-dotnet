import Foundation
import ActitoKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoNativeBinding)
public class ActitoNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.delegate = self
    }

    // MARK: - Actito

    @objc
    public var isConfigured: Bool {
        Actito.shared.isConfigured
    }

    @objc
    public var isReady: Bool {
        Actito.shared.isReady
    }

    @objc
    public var application: ActitoApplication? {
        Actito.shared.application.map { ActitoApplication(from: $0) }
    }

    @objc
    public var canEvaluateDeferredLink: Bool {
        Actito.shared.canEvaluateDeferredLink
    }

    @objc
    public weak var delegate: ActitoNativeBindingDelegate?

    @MainActor @objc
    public func configure() {
        Actito.shared.configure()
    }

    @MainActor @objc
    public func configure(applicationKey: String, applicationSecret: String) {
        Actito.shared.configure(
            servicesInfo: ActitoKit.ActitoServicesInfo(
                applicationKey: applicationKey,
                applicationSecret: applicationSecret
            )
        )
    }

    @objc
    public func launch(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.launch { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func unlaunch(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.unlaunch { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchApplication(_ onSuccess: @escaping SuccessBlock<ActitoApplication>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.fetchApplication { result in
            switch result {
            case let .success(application):
                onSuccess(ActitoApplication(from: application))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchNotification(_ id: String, _ onSuccess: @escaping SuccessBlock<ActitoNotification>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.fetchNotification(id) { result in
            switch result {
            case let .success(notification):
                onSuccess(ActitoNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchDynamicLink(_ url: String, _ onSuccess: @escaping SuccessBlock<ActitoDynamicLink>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.fetchDynamicLink(url) { result in
            switch result {
            case let .success(link):
                onSuccess(ActitoDynamicLink(from: link))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func evaluateDeferredLink(_ onSuccess: @escaping SuccessBlock<Bool>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.evaluateDeferredLink { result in
            switch result {
            case let .success(evaluated):
                onSuccess(evaluated)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func handleTestDeviceUrl(_ url: URL) -> Bool {
        return Actito.shared.handleTestDeviceUrl(url)
    }

    @objc
    public func handleDynamicLinkUrl(_ url: URL) -> Bool {
        return Actito.shared.handleDynamicLinkUrl(url)
    }

    // MARK: - Actito Device

    @objc
    public var currentDevice: ActitoDevice? {
        return Actito.shared.device().currentDevice.map { ActitoDevice(from: $0) }
    }

    @objc
    public func updateUser(userId: String?, userName: String?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().updateUser(userId: userId, userName: userName) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchTags(_ onSuccess: @escaping SuccessBlock<[String]>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().fetchTags { result in
            switch result {
            case let .success(tags):
                onSuccess(tags)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func addTag(_ tag: String, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().addTag(tag) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func addTags(_ tags: [String], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().addTags(tags) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func removeTag(_ tag: String, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().removeTag(tag) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func removeTags(_ tags: [String], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().removeTags(tags) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func clearTags(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().clearTags { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public var preferredLanguage: String? {
        Actito.shared.device().preferredLanguage
    }

    @objc
    public func updatePreferredLanguage(_ preferredLanguage: String?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().updatePreferredLanguage(preferredLanguage) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchDoNotDisturb(_ onSuccess: @escaping SuccessBlock<ActitoDoNotDisturb?>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().fetchDoNotDisturb { result in
            switch result {
            case let .success(dnd):
                onSuccess(dnd.map { ActitoDoNotDisturb(from: $0) })
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func updateDoNotDisturb(_ dnd: ActitoDoNotDisturb, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        do {
            Actito.shared.device().updateDoNotDisturb(try dnd.toNative()) { result in
                switch result {
                case .success:
                    onSuccess()
                case let .failure(error):
                    onFailure(error)
                }
            }
        } catch {
            onFailure(error)
        }
    }

    @objc
    public func clearDoNotDisturb(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().clearDoNotDisturb { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchUserData(_ onSuccess: @escaping SuccessBlock<[String : String]>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.device().fetchUserData { result in
            switch result {
            case let .success(userData):
                onSuccess(userData)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func updateUserData(_ userData: [String : Any], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        let data = userData.mapValues { value in
            return value as? String
        }

        Actito.shared.device().updateUserData(data) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    // MARK: - Actito Events

    @objc
    public func logCustom(_ eventName: String, data: [String : Any]?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.events().logCustom(eventName, data: data) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension ActitoNativeBinding : ActitoDelegate {
    public func actito(_ actito: ActitoKit.Actito, onReady application: ActitoKit.ActitoApplication) {
        delegate?.actito(self, onReady: ActitoApplication(from: application))
    }

    public func actitoDidUnlaunch(_ actito: Actito) {
        delegate?.actitoDidUnlaunch(self)
    }

    public func actito(_ actito: ActitoKit.Actito, didRegisterDevice device: ActitoKit.ActitoDevice) {
        delegate?.actito(self, didRegisterDevice: ActitoDevice(from: device))
    }
}

@objc
public protocol ActitoNativeBindingDelegate : NSObjectProtocol {
    func actito(_ actito: ActitoNativeBinding, onReady application: ActitoApplication)

    func actitoDidUnlaunch(_ actito: ActitoNativeBinding)

    func actito(_ actito: ActitoNativeBinding, didRegisterDevice device: ActitoDevice)
}
