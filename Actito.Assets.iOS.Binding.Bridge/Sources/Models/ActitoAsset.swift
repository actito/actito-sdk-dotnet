import Foundation
import ActitoAssetsKit

@objc
public class ActitoAsset : NSObject {
    @objc public let title: String
    @objc public let assetDescription: String?
    @objc public let key: String?
    @objc public let url: String?
    @objc public let button: ActitoAssetButton?
    @objc public let metaData: ActitoAssetMetaData?
    @objc public let extra: [String: Any]

    @objc public init(title: String, assetDescription: String?, key: String?, url: String?, button: ActitoAssetButton?, metaData: ActitoAssetMetaData?, extra: [String : Any]) {
        self.title = title
        self.assetDescription = assetDescription
        self.key = key
        self.url = url
        self.button = button
        self.metaData = metaData
        self.extra = extra
    }

    public convenience init(from asset: ActitoAssetsKit.ActitoAsset) {
        self.init(
            title: asset.title,
            assetDescription: asset.description,
            key: asset.key,
            url: asset.url,
            button: asset.button.map { ActitoAssetButton(from: $0) },
            metaData: asset.metaData.map { ActitoAssetMetaData(from: $0) },
            extra: asset.extra
        )
    }
}

@objc
public class ActitoAssetButton : NSObject {
    @objc public let label: String?
    @objc public let action: String?

    @objc public init(label: String?, action: String?) {
        self.label = label
        self.action = action
    }

    public convenience init(from button: ActitoAssetsKit.ActitoAsset.Button) {
        self.init(
            label: button.label,
            action: button.action
        )
    }
}

@objc
public class ActitoAssetMetaData : NSObject {
    @objc public let originalFileName: String
    @objc public let contentType: String
    @objc public let contentLength: Int

    @objc public init(originalFileName: String, contentType: String, contentLength: Int) {
        self.originalFileName = originalFileName
        self.contentType = contentType
        self.contentLength = contentLength
    }

    public convenience init(from metaData: ActitoAssetsKit.ActitoAsset.MetaData) {
        self.init(
            originalFileName: metaData.originalFileName,
            contentType: metaData.contentType,
            contentLength: metaData.contentLength
        )
    }
}
