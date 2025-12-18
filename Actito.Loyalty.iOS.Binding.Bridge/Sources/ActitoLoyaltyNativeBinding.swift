import Foundation
import UIKit
import ActitoKit
import ActitoLoyaltyKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@MainActor
@objc(ActitoLoyaltyNativeBinding)
public class ActitoLoyaltyNativeBinding : NSObject {

    @objc
    public func fetchPassBySerial(_ serial: String, _ onSuccess: @escaping SuccessBlock<ActitoPass>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.loyalty().fetchPass(serial: serial) { result in
            switch result {
            case let .success(pass):
                onSuccess(ActitoPass(from: pass))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchPassByBarcode(_ barcode: String, _ onSuccess: @escaping SuccessBlock<ActitoPass>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.loyalty().fetchPass(barcode: barcode) { result in
            switch result {
            case let .success(pass):
                onSuccess(ActitoPass(from: pass))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func present(_ pass: ActitoPass, in controller: UIViewController) {
        Actito.shared.loyalty().present(pass: pass.toNative(), in: controller)
    }
}
