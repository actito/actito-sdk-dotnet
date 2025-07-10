import Foundation
import ActitoKit

@objc
public class ActitoApplication : NSObject {
    @objc public let applicationId: String
    @objc public let name: String
    @objc public let category: String
    @objc public let appStoreId: String?
    @objc public let services: [String : Bool]
    @objc public let inboxConfig: ActitoApplicationInboxConfig?
    @objc public let regionConfig: ActitoApplicationRegionConfig?
    @objc public let userDataFields: [ActitoApplicationUserDataField]
    @objc public let actionCategories: [ActitoApplicationActionCategory]

    @objc public init(applicationId: String, name: String, category: String, appStoreId: String?, services: [String : Bool], inboxConfig: ActitoApplicationInboxConfig?, regionConfig: ActitoApplicationRegionConfig?, userDataFields: [ActitoApplicationUserDataField], actionCategories: [ActitoApplicationActionCategory]) {
        self.applicationId = applicationId
        self.name = name
        self.category = category
        self.appStoreId = appStoreId
        self.services = services
        self.inboxConfig = inboxConfig
        self.regionConfig = regionConfig
        self.userDataFields = userDataFields
        self.actionCategories = actionCategories
    }

    public convenience init(from application: ActitoKit.ActitoApplication) {
        self.init(
            applicationId: application.id,
            name: application.name,
            category: application.category,
            appStoreId: application.appStoreId,
            services: application.services,
            inboxConfig: application.inboxConfig.map { ActitoApplicationInboxConfig(from: $0) },
            regionConfig: application.regionConfig.map { ActitoApplicationRegionConfig(from: $0) },
            userDataFields: application.userDataFields.map { ActitoApplicationUserDataField(from: $0) },
            actionCategories: application.actionCategories.map { ActitoApplicationActionCategory(from: $0) }
        )
    }
}

@objc
public class ActitoApplicationInboxConfig : NSObject {
    @objc public let useInbox: Bool
    @objc public let useUserInbox: Bool
    @objc public let autoBadge: Bool

    @objc public init(useInbox: Bool, useUserInbox: Bool, autoBadge: Bool) {
        self.useInbox = useInbox
        self.useUserInbox = useUserInbox
        self.autoBadge = autoBadge
    }

    public convenience init(from inboxConfig: ActitoKit.ActitoApplication.InboxConfig) {
        self.init(
            useInbox: inboxConfig.useInbox,
            useUserInbox: inboxConfig.useUserInbox,
            autoBadge: inboxConfig.autoBadge
        )
    }
}

@objc
public class ActitoApplicationRegionConfig : NSObject {
    @objc public let proximityUUID: String?

    @objc public init(proximityUUID: String?) {
        self.proximityUUID = proximityUUID
    }

    public convenience init(from regionConfig: ActitoKit.ActitoApplication.RegionConfig) {
        self.init(proximityUUID: regionConfig.proximityUUID)
    }
}

@objc
public class ActitoApplicationUserDataField : NSObject {
    @objc public let type: String
    @objc public let key: String
    @objc public let label: String

    @objc public init(type: String, key: String, label: String) {
        self.type = type
        self.key = key
        self.label = label
    }

    public convenience init(from userDataField: ActitoKit.ActitoApplication.UserDataField) {
        self.init(
            type: userDataField.type,
            key: userDataField.key,
            label: userDataField.label
        )
    }
}

@objc
public class ActitoApplicationActionCategory : NSObject {
    @objc public let name: String
    @objc public let actionCategoryDescription: String?
    @objc public let type: String
    @objc public let actions: [ActitoNotificationAction]

    @objc public init(name: String, actionCategoryDescription: String?, type: String, actions: [ActitoNotificationAction]) {
        self.name = name
        self.actionCategoryDescription = actionCategoryDescription
        self.type = type
        self.actions = actions
    }

    public convenience init(from actionCategory: ActitoKit.ActitoApplication.ActionCategory) {
        self.init(
            name: actionCategory.name,
            actionCategoryDescription: actionCategory.description,
            type: actionCategory.type,
            actions: actionCategory.actions.map { ActitoNotificationAction(from: $0) }
        )
    }
}
