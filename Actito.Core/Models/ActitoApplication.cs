namespace ActitoSdk.Core.Models;

/// <summary>
/// Represents an Actito application.
/// </summary>
/// <remarks>
/// An <see cref="ActitoApplication"/> describes the capabilities, services, and configuration
/// of an application as defined in Actito. It includes enabled services, region and
/// inbox configuration, available user data fields, and supported action categories.
/// </remarks>
public class ActitoApplication
{
    /// <summary>
    /// Unique identifier of the application.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Name of the application.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Category of the application.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// The App Store ID of the application
    /// </summary>
    public string? AppStoreId { get; }

    /// <summary>
    /// Dictionary of enabled services for the application.
    /// </summary>
    public IDictionary<string, bool> Services { get; }

    /// <summary>
    /// Optional inbox-related configuration.
    /// </summary>
    public ActitoApplicationInboxConfig? InboxConfig { get; }

    /// <summary>
    /// Optional region-related configuration.
    /// </summary>
    public ActitoApplicationRegionConfig? RegionConfig { get; }

    /// <summary>
    /// List of user data fields supported by the application.
    /// </summary>
    public IList<ActitoApplicationUserDataField> UserDataFields { get; }

    /// <summary>
    /// List of action categories available in the application.
    /// </summary>
    public IList<ActitoApplicationActionCategory> ActionCategories { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoApplication"/>.
    /// </summary>
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

/// <summary>
/// Configuration related to inbox-based features.
/// </summary>
public class ActitoApplicationInboxConfig
{
    /// <summary>
    /// Whether the inbox feature is enabled for the application.
    /// </summary>
    public bool UseInbox { get; }

    /// <summary>
    /// Whether the user inbox feature is enabled for the application.
    /// </summary>
    public bool UseUserInbox { get; }

    /// <summary>
    /// Whether inbox messages should automatically update the application badge count.
    /// </summary>
    public bool AutoBadge { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoApplicationInboxConfig"/>.
    /// </summary>
    public ActitoApplicationInboxConfig(bool useInbox, bool useUserInbox, bool autoBadge)
    {
        UseInbox = useInbox;
        UseUserInbox = useUserInbox;
        AutoBadge = autoBadge;
    }
}

/// <summary>
/// Configuration related to region-based features.
/// </summary>
public class ActitoApplicationRegionConfig
{
    /// <summary>
    /// Optional UUID used for beacon detection.
    /// </summary>
    public string? ProximityUUID { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoApplicationRegionConfig"/>.
    /// </summary>
    public ActitoApplicationRegionConfig(string? proximityUUID)
    {
        ProximityUUID = proximityUUID;
    }
}

/// <summary>
/// Describes a user data field supported by an Actito application.
/// </summary>
/// <remarks>
/// User data fields define the structure of user attributes that can be 
/// stored and leveraged for segmentation or personalization.
/// </remarks>
public class ActitoApplicationUserDataField
{
    /// <summary>
    /// The data type of the field.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The unique key identifying the field.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Human-readable label for the field.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoApplicationUserDataField"/>.
    /// </summary>
    public ActitoApplicationUserDataField(string type, string key, string label)
    {
        Type = type;
        Key = key;
        Label = label;
    }
}

/// <summary>
/// Groups related actions that can be triggered from notifications or other engagement mechanisms.
/// </summary>
public class ActitoApplicationActionCategory
{
    /// <summary>
    /// The category type identifier.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The name of the action category.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Optional description explaining the purpose of the category.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// List of actions belonging to this category.
    /// </summary>
    public IList<ActitoNotificationAction> Actions { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoApplicationActionCategory"/>.
    /// </summary>
    public ActitoApplicationActionCategory(string name, string? description, string type, IList<ActitoNotificationAction> actions)
    {
        Name = name;
        Description = description;
        Type = type;
        Actions = actions;
    }
}
