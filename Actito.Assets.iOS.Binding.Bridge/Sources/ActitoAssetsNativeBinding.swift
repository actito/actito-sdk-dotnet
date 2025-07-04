import Foundation
import ActitoKit
import ActitoAssetsKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoAssetsNativeBinding)
public class ActitoAssetsNativeBinding : NSObject {

    @objc
    public func fetch(_ group: String, _ onSuccess: @escaping SuccessBlock<[ActitoAsset]>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.assets().fetch(group: group) { result in
            switch result {
            case let .success(assets):
                onSuccess(assets.map { ActitoAsset(from: $0) })
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
