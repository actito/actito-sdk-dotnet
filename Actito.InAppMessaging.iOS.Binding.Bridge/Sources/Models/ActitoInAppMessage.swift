import Foundation
import ActitoInAppMessagingKit

@objc
public class ActitoInAppMessage : NSObject {
    @objc public let inAppMessageId: String
    @objc public let name: String
    @objc public let type: String
    @objc public let context: [String]
    @objc public let title: String?
    @objc public let message: String?
    @objc public let image: String?
    @objc public let landscapeImage: String?
    @objc public let delaySeconds: Int
    @objc public let primaryAction: ActitoInAppMessageAction?
    @objc public let secondaryAction: ActitoInAppMessageAction?

    @objc public init(inAppMessageId: String, name: String, type: String, context: [String], title: String?, message: String?, image: String?, landscapeImage: String?, delaySeconds: Int, primaryAction: ActitoInAppMessageAction?, secondaryAction: ActitoInAppMessageAction?) {
        self.inAppMessageId = inAppMessageId
        self.name = name
        self.type = type
        self.context = context
        self.title = title
        self.message = message
        self.image = image
        self.landscapeImage = landscapeImage
        self.delaySeconds = delaySeconds
        self.primaryAction = primaryAction
        self.secondaryAction = secondaryAction
    }

    public convenience init(from message: ActitoInAppMessagingKit.ActitoInAppMessage) {
        self.init(
            inAppMessageId: message.id,
            name: message.name,
            type: message.type,
            context: message.context,
            title: message.title,
            message: message.message,
            image: message.image,
            landscapeImage: message.landscapeImage,
            delaySeconds: message.delaySeconds,
            primaryAction: message.primaryAction.map { ActitoInAppMessageAction(from: $0) },
            secondaryAction: message.secondaryAction.map { ActitoInAppMessageAction(from: $0) }
        )
    }
}

@objc
public class ActitoInAppMessageAction : NSObject {
    @objc public let label: String?
    @objc public let destructive: Bool
    @objc public let url: String?

    @objc public init(label: String?, destructive: Bool, url: String?) {
        self.label = label
        self.destructive = destructive
        self.url = url
    }

    public convenience init(from action: ActitoInAppMessagingKit.ActitoInAppMessage.Action) {
        self.init(
            label: action.label,
            destructive: action.destructive,
            url: action.url
        )
    }
}
