namespace ActitoSdk.Core.Models;

public class ActitoApplication
{
    public string Id { get; }
    public string Name { get; }
    public string Category { get; }
    public string? AppStoreId { get; }
    public IDictionary<string, bool> Services { get; }
    public ActitoApplicationInboxConfig? InboxConfig { get; }
    public ActitoApplicationRegionConfig? RegionConfig { get; }
    public IList<ActitoApplicationUserDataField> UserDataFields { get; }
    public IList<ActitoApplicationActionCategory> ActionCategories { get; }

    public ActitoApplication(string id, string name, string category, string? appStoreId, IDictionary<string, bool> services, ActitoApplicationInboxConfig? inboxConfig, ActitoApplicationRegionConfig? regionConfig, IList<ActitoApplicationUserDataField> userDataFields, IList<ActitoApplicationActionCategory> actionCategories)
    {
        Id = id;
        Name = name;
        Category = category;
        AppStoreId = appStoreId;
        Services = services;
        InboxConfig = inboxConfig;
        RegionConfig = regionConfig;
        UserDataFields = userDataFields;
        ActionCategories = actionCategories;
    }
}

public class ActitoApplicationInboxConfig
{
    public bool UseInbox { get; }
    public bool UseUserInbox { get; }
    public bool AutoBadge { get; }

    public ActitoApplicationInboxConfig(bool useInbox, bool useUserInbox, bool autoBadge)
    {
        UseInbox = useInbox;
        UseUserInbox = useUserInbox;
        AutoBadge = autoBadge;
    }
}

public class ActitoApplicationRegionConfig
{
    public string? ProximityUUID { get; }

    public ActitoApplicationRegionConfig(string? proximityUUID)
    {
        ProximityUUID = proximityUUID;
    }
}


public class ActitoApplicationUserDataField
{
    public string Type { get; }
    public string Key { get; }
    public string Label { get; }

    public ActitoApplicationUserDataField(string type, string key, string label)
    {
        Type = type;
        Key = key;
        Label = label;
    }
}

public class ActitoApplicationActionCategory
{
    public string Name { get; }
    public string? Description { get; }
    public string Type { get; }
    public IList<ActitoNotificationAction> Actions { get; }

    public ActitoApplicationActionCategory(string name, string? description, string type, IList<ActitoNotificationAction> actions)
    {
        Name = name;
        Description = description;
        Type = type;
        Actions = actions;
    }
}
