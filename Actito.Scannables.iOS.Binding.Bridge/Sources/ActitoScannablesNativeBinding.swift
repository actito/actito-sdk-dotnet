import Foundation
import UIKit
import ActitoKit
import ActitoScannablesKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(ActitoScannablesNativeBinding)
public class ActitoScannablesNativeBinding : NSObject {

    public override init() {
        super.init()

        Actito.shared.scannables().delegate = self
    }

    @objc
    public var canStartNfcScannableSession: Bool {
        Actito.shared.scannables().canStartNfcScannableSession
    }

    @objc
    public weak var delegate: ActitoScannablesNativeBindingDelegate?

    @objc
    public func startScannableSession(_ controller: UIViewController) {
        Actito.shared.scannables().startScannableSession(controller: controller)
    }

    @objc
    public func startNfcScannableSession() {
        Actito.shared.scannables().startNfcScannableSession()
    }

    @objc
    public func startQrCodeScannableSession(_ controller: UIViewController, modal: Bool) {
        Actito.shared.scannables().startQrCodeScannableSession(controller: controller, modal: modal)
    }

    @objc
    public func fetch(_ tag: String, _ onSuccess: @escaping SuccessBlock<ActitoScannable>, _ onFailure: @escaping ErrorBlock) {
        Actito.shared.scannables().fetch(tag: tag) { result in
            switch result {
            case let .success(scannable):
                onSuccess(ActitoScannable(from: scannable))
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension ActitoScannablesNativeBinding : ActitoScannablesDelegate {
    public func actito(_ actitoScannables: any ActitoScannablesKit.ActitoScannables, didInvalidateScannerSession error: any Error) {
        delegate?.actito(self, didInvalidateScannerSession: error)
    }

    public func actito(_ actitoScannables: any ActitoScannablesKit.ActitoScannables, didDetectScannable scannable: ActitoScannablesKit.ActitoScannable) {
        delegate?.actito(self, didDetectScannable: ActitoScannable(from: scannable))
    }
}

@objc
public protocol ActitoScannablesNativeBindingDelegate : NSObjectProtocol {
    func actito(_ actitoScannables: ActitoScannablesNativeBinding, didInvalidateScannerSession error: any Error)

    func actito(_ actitoScannables: ActitoScannablesNativeBinding, didDetectScannable scannable: ActitoScannable)
}
