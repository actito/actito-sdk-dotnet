using ActitoSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.iOS.Binding
{
	// @interface ActitoApplication : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding17ActitoApplication")]
	[DisableDefaultCtor]
	interface ActitoApplication
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull applicationId;
		[Export ("applicationId")]
		string ApplicationId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull category;
		[Export ("category")]
		string Category { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable appStoreId;
		[NullAllowed, Export ("appStoreId")]
		string AppStoreId { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSNumber *> * _Nonnull services;
		[Export ("services", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSNumber> Services { get; }

		// @property (readonly, nonatomic, strong) ActitoApplicationInboxConfig * _Nullable inboxConfig;
		[NullAllowed, Export ("inboxConfig", ArgumentSemantic.Strong)]
		ActitoApplicationInboxConfig InboxConfig { get; }

		// @property (readonly, nonatomic, strong) ActitoApplicationRegionConfig * _Nullable regionConfig;
		[NullAllowed, Export ("regionConfig", ArgumentSemantic.Strong)]
		ActitoApplicationRegionConfig RegionConfig { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoApplicationUserDataField *> * _Nonnull userDataFields;
		[Export ("userDataFields", ArgumentSemantic.Copy)]
		ActitoApplicationUserDataField[] UserDataFields { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoApplicationActionCategory *> * _Nonnull actionCategories;
		[Export ("actionCategories", ArgumentSemantic.Copy)]
		ActitoApplicationActionCategory[] ActionCategories { get; }

		// -(instancetype _Nonnull)initWithApplicationId:(NSString * _Nonnull)applicationId name:(NSString * _Nonnull)name category:(NSString * _Nonnull)category appStoreId:(NSString * _Nullable)appStoreId services:(NSDictionary<NSString *,NSNumber *> * _Nonnull)services inboxConfig:(ActitoApplicationInboxConfig * _Nullable)inboxConfig regionConfig:(ActitoApplicationRegionConfig * _Nullable)regionConfig userDataFields:(NSArray<ActitoApplicationUserDataField *> * _Nonnull)userDataFields actionCategories:(NSArray<ActitoApplicationActionCategory *> * _Nonnull)actionCategories __attribute__((objc_designated_initializer));
		[Export ("initWithApplicationId:name:category:appStoreId:services:inboxConfig:regionConfig:userDataFields:actionCategories:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string applicationId, string name, string category, [NullAllowed] string appStoreId, NSDictionary<NSString, NSNumber> services, [NullAllowed] ActitoApplicationInboxConfig inboxConfig, [NullAllowed] ActitoApplicationRegionConfig regionConfig, ActitoApplicationUserDataField[] userDataFields, ActitoApplicationActionCategory[] actionCategories);
	}

	// @interface ActitoApplicationActionCategory : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding31ActitoApplicationActionCategory")]
	[DisableDefaultCtor]
	interface ActitoApplicationActionCategory
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable actionCategoryDescription;
		[NullAllowed, Export ("actionCategoryDescription")]
		string ActionCategoryDescription { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoNotificationAction *> * _Nonnull actions;
		[Export ("actions", ArgumentSemantic.Copy)]
		ActitoNotificationAction[] Actions { get; }

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name actionCategoryDescription:(NSString * _Nullable)actionCategoryDescription type:(NSString * _Nonnull)type actions:(NSArray<ActitoNotificationAction *> * _Nonnull)actions __attribute__((objc_designated_initializer));
		[Export ("initWithName:actionCategoryDescription:type:actions:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string name, [NullAllowed] string actionCategoryDescription, string type, ActitoNotificationAction[] actions);
	}

	// @interface ActitoApplicationInboxConfig : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding28ActitoApplicationInboxConfig")]
	[DisableDefaultCtor]
	interface ActitoApplicationInboxConfig
	{
		// @property (readonly, nonatomic) BOOL useInbox;
		[Export ("useInbox")]
		bool UseInbox { get; }

		// @property (readonly, nonatomic) BOOL useUserInbox;
		[Export ("useUserInbox")]
		bool UseUserInbox { get; }

		// @property (readonly, nonatomic) BOOL autoBadge;
		[Export ("autoBadge")]
		bool AutoBadge { get; }

		// -(instancetype _Nonnull)initWithUseInbox:(BOOL)useInbox useUserInbox:(BOOL)useUserInbox autoBadge:(BOOL)autoBadge __attribute__((objc_designated_initializer));
		[Export ("initWithUseInbox:useUserInbox:autoBadge:")]
		[DesignatedInitializer]
		NativeHandle Constructor (bool useInbox, bool useUserInbox, bool autoBadge);
	}

	// @interface ActitoApplicationRegionConfig : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding29ActitoApplicationRegionConfig")]
	[DisableDefaultCtor]
	interface ActitoApplicationRegionConfig
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable proximityUUID;
		[NullAllowed, Export ("proximityUUID")]
		string ProximityUUID { get; }

		// -(instancetype _Nonnull)initWithProximityUUID:(NSString * _Nullable)proximityUUID __attribute__((objc_designated_initializer));
		[Export ("initWithProximityUUID:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string proximityUUID);
	}

	// @interface ActitoApplicationUserDataField : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding30ActitoApplicationUserDataField")]
	[DisableDefaultCtor]
	interface ActitoApplicationUserDataField
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull key;
		[Export ("key")]
		string Key { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull label;
		[Export ("label")]
		string Label { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type key:(NSString * _Nonnull)key label:(NSString * _Nonnull)label __attribute__((objc_designated_initializer));
		[Export ("initWithType:key:label:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, string key, string label);
	}

	// @interface ActitoDevice : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding12ActitoDevice")]
	[DisableDefaultCtor]
	interface ActitoDevice
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull deviceId;
		[Export ("deviceId")]
		string DeviceId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
		[NullAllowed, Export ("userId")]
		string UserId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable userName;
		[NullAllowed, Export ("userName")]
		string UserName { get; }

		// @property (readonly, nonatomic) float timeZoneOffset;
		[Export ("timeZoneOffset")]
		float TimeZoneOffset { get; }

		// @property (readonly, nonatomic, strong) ActitoDoNotDisturb * _Nullable dnd;
		[NullAllowed, Export ("dnd", ArgumentSemantic.Strong)]
		ActitoDoNotDisturb Dnd { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nonnull userData;
		[Export ("userData", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> UserData { get; }

		// @property (readonly, nonatomic) BOOL backgroundAppRefresh;
		[Export ("backgroundAppRefresh")]
		bool BackgroundAppRefresh { get; }

		// -(instancetype _Nonnull)initWithDeviceId:(NSString * _Nonnull)deviceId userId:(NSString * _Nullable)userId userName:(NSString * _Nullable)userName timeZoneOffset:(float)timeZoneOffset dnd:(ActitoDoNotDisturb * _Nullable)dnd userData:(NSDictionary<NSString *,NSString *> * _Nonnull)userData backgroundAppRefresh:(BOOL)backgroundAppRefresh __attribute__((objc_designated_initializer));
		[Export ("initWithDeviceId:userId:userName:timeZoneOffset:dnd:userData:backgroundAppRefresh:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string deviceId, [NullAllowed] string userId, [NullAllowed] string userName, float timeZoneOffset, [NullAllowed] ActitoDoNotDisturb dnd, NSDictionary<NSString, NSString> userData, bool backgroundAppRefresh);
	}

	// @interface ActitoDoNotDisturb : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding18ActitoDoNotDisturb")]
	[DisableDefaultCtor]
	interface ActitoDoNotDisturb
	{
		// @property (readonly, nonatomic, strong) ActitoTime * _Nonnull start;
		[Export ("start", ArgumentSemantic.Strong)]
		ActitoTime Start { get; }

		// @property (readonly, nonatomic, strong) ActitoTime * _Nonnull end;
		[Export ("end", ArgumentSemantic.Strong)]
		ActitoTime End { get; }

		// -(instancetype _Nonnull)initWithStart:(ActitoTime * _Nonnull)start end:(ActitoTime * _Nonnull)end __attribute__((objc_designated_initializer));
		[Export ("initWithStart:end:")]
		[DesignatedInitializer]
		NativeHandle Constructor (ActitoTime start, ActitoTime end);
	}

	// @interface ActitoDynamicLink : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding17ActitoDynamicLink")]
	[DisableDefaultCtor]
	interface ActitoDynamicLink
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull target;
		[Export ("target")]
		string Target { get; }

		// -(instancetype _Nonnull)initWithTarget:(NSString * _Nonnull)target __attribute__((objc_designated_initializer));
		[Export ("initWithTarget:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string target);
	}

	// @interface ActitoNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoNativeBinding
	{
		// @property (readonly, nonatomic) BOOL isConfigured;
		[Export ("isConfigured")]
		bool IsConfigured { get; }

		// @property (readonly, nonatomic) BOOL isReady;
		[Export ("isReady")]
		bool IsReady { get; }

		// @property (readonly, nonatomic, strong) ActitoApplication * _Nullable application;
		[NullAllowed, Export ("application", ArgumentSemantic.Strong)]
		ActitoApplication Application { get; }

		// @property (readonly, nonatomic) BOOL canEvaluateDeferredLink;
		[Export ("canEvaluateDeferredLink")]
		bool CanEvaluateDeferredLink { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)configure;
		[Export ("configure")]
		void Configure ();

		// -(void)configureWithApplicationKey:(NSString * _Nonnull)applicationKey applicationSecret:(NSString * _Nonnull)applicationSecret;
		[Export ("configureWithApplicationKey:applicationSecret:")]
		void ConfigureWithApplicationKey (string applicationKey, string applicationSecret);

		// -(void)launch:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("launch::")]
		void Launch (Action onSuccess, Action<NSError> onFailure);

		// -(void)unlaunch:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("unlaunch::")]
		void Unlaunch (Action onSuccess, Action<NSError> onFailure);

		// -(void)fetchApplication:(void (^ _Nonnull)(ActitoApplication * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchApplication::")]
		void FetchApplication (Action<ActitoApplication> onSuccess, Action<NSError> onFailure);

		// -(void)fetchNotification:(NSString * _Nonnull)id :(void (^ _Nonnull)(ActitoNotification * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchNotification:::")]
		void FetchNotification (string id, Action<ActitoNotification> onSuccess, Action<NSError> onFailure);

		// -(void)fetchDynamicLink:(NSString * _Nonnull)url :(void (^ _Nonnull)(ActitoDynamicLink * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchDynamicLink:::")]
		void FetchDynamicLink (string url, Action<ActitoDynamicLink> onSuccess, Action<NSError> onFailure);

		// -(void)evaluateDeferredLink:(void (^ _Nonnull)(BOOL))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("evaluateDeferredLink::")]
		void EvaluateDeferredLink (Action<bool> onSuccess, Action<NSError> onFailure);

		// -(BOOL)handleTestDeviceUrl:(NSURL * _Nonnull)url __attribute__((warn_unused_result("")));
		[Export ("handleTestDeviceUrl:")]
		bool HandleTestDeviceUrl (NSUrl url);

		// -(BOOL)handleDynamicLinkUrl:(NSURL * _Nonnull)url __attribute__((warn_unused_result("")));
		[Export ("handleDynamicLinkUrl:")]
		bool HandleDynamicLinkUrl (NSUrl url);

		// @property (readonly, nonatomic, strong) ActitoDevice * _Nullable currentDevice;
		[NullAllowed, Export ("currentDevice", ArgumentSemantic.Strong)]
		ActitoDevice CurrentDevice { get; }

		// -(void)updateUserWithUserId:(NSString * _Nullable)userId userName:(NSString * _Nullable)userName :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("updateUserWithUserId:userName:::")]
		void UpdateUserWithUserId ([NullAllowed] string userId, [NullAllowed] string userName, Action onSuccess, Action<NSError> onFailure);

		// -(void)fetchTags:(void (^ _Nonnull)(NSArray<NSString *> * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchTags::")]
		void FetchTags (Action<NSArray<NSString>> onSuccess, Action<NSError> onFailure);

		// -(void)addTag:(NSString * _Nonnull)tag :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("addTag:::")]
		void AddTag (string tag, Action onSuccess, Action<NSError> onFailure);

		// -(void)addTags:(NSArray<NSString *> * _Nonnull)tags :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("addTags:::")]
		void AddTags (string[] tags, Action onSuccess, Action<NSError> onFailure);

		// -(void)removeTag:(NSString * _Nonnull)tag :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("removeTag:::")]
		void RemoveTag (string tag, Action onSuccess, Action<NSError> onFailure);

		// -(void)removeTags:(NSArray<NSString *> * _Nonnull)tags :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("removeTags:::")]
		void RemoveTags (string[] tags, Action onSuccess, Action<NSError> onFailure);

		// -(void)clearTags:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("clearTags::")]
		void ClearTags (Action onSuccess, Action<NSError> onFailure);

		// @property (readonly, copy, nonatomic) NSString * _Nullable preferredLanguage;
		[NullAllowed, Export ("preferredLanguage")]
		string PreferredLanguage { get; }

		// -(void)updatePreferredLanguage:(NSString * _Nullable)preferredLanguage :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("updatePreferredLanguage:::")]
		void UpdatePreferredLanguage ([NullAllowed] string preferredLanguage, Action onSuccess, Action<NSError> onFailure);

		// -(void)fetchDoNotDisturb:(void (^ _Nonnull)(ActitoDoNotDisturb * _Nullable))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchDoNotDisturb::")]
		void FetchDoNotDisturb (Action<ActitoDoNotDisturb> onSuccess, Action<NSError> onFailure);

		// -(void)updateDoNotDisturb:(ActitoDoNotDisturb * _Nonnull)dnd :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("updateDoNotDisturb:::")]
		void UpdateDoNotDisturb (ActitoDoNotDisturb dnd, Action onSuccess, Action<NSError> onFailure);

		// -(void)clearDoNotDisturb:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("clearDoNotDisturb::")]
		void ClearDoNotDisturb (Action onSuccess, Action<NSError> onFailure);

		// -(void)fetchUserData:(void (^ _Nonnull)(NSDictionary<NSString *,NSString *> * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchUserData::")]
		void FetchUserData (Action<NSDictionary<NSString, NSString>> onSuccess, Action<NSError> onFailure);

		// -(void)updateUserData:(NSDictionary<NSString *,id> * _Nonnull)userData :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("updateUserData:::")]
		void UpdateUserData (NSDictionary<NSString, NSObject> userData, Action onSuccess, Action<NSError> onFailure);

		// -(void)logCustom:(NSString * _Nonnull)eventName data:(NSDictionary<NSString *,id> * _Nullable)data :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("logCustom:data:::")]
		void LogCustom (string eventName, [NullAllowed] NSDictionary<NSString, NSObject> data, Action onSuccess, Action<NSError> onFailure);
	}

	// @protocol ActitoNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP13ActitoBinding27ActitoNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP13ActitoBinding27ActitoNativeBindingDelegate_")]
	interface ActitoNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoNativeBinding * _Nonnull)actito onReady:(ActitoApplication * _Nonnull)application;
		[Abstract]
		[Export ("actito:onReady:")]
		void Actito (ActitoNativeBinding actito, ActitoApplication application);

		// @required -(void)actitoDidUnlaunch:(ActitoNativeBinding * _Nonnull)actito;
		[Abstract]
		[Export ("actitoDidUnlaunch:")]
		void ActitoDidUnlaunch (ActitoNativeBinding actito);

		// @required -(void)actito:(ActitoNativeBinding * _Nonnull)actito didRegisterDevice:(ActitoDevice * _Nonnull)device;
		[Abstract]
		[Export ("actito:didRegisterDevice:")]
		void Actito (ActitoNativeBinding actito, ActitoDevice device);
	}

	// @interface ActitoNotification : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding18ActitoNotification")]
	[DisableDefaultCtor]
	interface ActitoNotification
	{
		// @property (readonly, nonatomic) BOOL partial;
		[Export ("partial")]
		bool Partial { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull notificationId;
		[Export ("notificationId")]
		string NotificationId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull time;
		[Export ("time", ArgumentSemantic.Copy)]
		NSDate Time { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable subtitle;
		[NullAllowed, Export ("subtitle")]
		string Subtitle { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull message;
		[Export ("message")]
		string Message { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoNotificationContent *> * _Nonnull content;
		[Export ("content", ArgumentSemantic.Copy)]
		ActitoNotificationContent[] Content { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoNotificationAction *> * _Nonnull actions;
		[Export ("actions", ArgumentSemantic.Copy)]
		ActitoNotificationAction[] Actions { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoNotificationAttachment *> * _Nonnull attachments;
		[Export ("attachments", ArgumentSemantic.Copy)]
		ActitoNotificationAttachment[] Attachments { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull extra;
		[Export ("extra", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Extra { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable targetContentIdentifier;
		[NullAllowed, Export ("targetContentIdentifier")]
		string TargetContentIdentifier { get; }

		// -(instancetype _Nonnull)initWithPartial:(BOOL)partial notificationId:(NSString * _Nonnull)notificationId type:(NSString * _Nonnull)type time:(NSDate * _Nonnull)time title:(NSString * _Nullable)title subtitle:(NSString * _Nullable)subtitle message:(NSString * _Nonnull)message content:(NSArray<ActitoNotificationContent *> * _Nonnull)content actions:(NSArray<ActitoNotificationAction *> * _Nonnull)actions attachments:(NSArray<ActitoNotificationAttachment *> * _Nonnull)attachments extra:(NSDictionary<NSString *,id> * _Nonnull)extra targetContentIdentifier:(NSString * _Nullable)targetContentIdentifier __attribute__((objc_designated_initializer));
		[Export ("initWithPartial:notificationId:type:time:title:subtitle:message:content:actions:attachments:extra:targetContentIdentifier:")]
		[DesignatedInitializer]
		NativeHandle Constructor (bool partial, string notificationId, string type, NSDate time, [NullAllowed] string title, [NullAllowed] string subtitle, string message, ActitoNotificationContent[] content, ActitoNotificationAction[] actions, ActitoNotificationAttachment[] attachments, NSDictionary<NSString, NSObject> extra, [NullAllowed] string targetContentIdentifier);
	}

	// @interface ActitoNotificationAction : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding24ActitoNotificationAction")]
	[DisableDefaultCtor]
	interface ActitoNotificationAction
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull label;
		[Export ("label")]
		string Label { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable target;
		[NullAllowed, Export ("target")]
		string Target { get; }

		// @property (readonly, nonatomic) BOOL keyboard;
		[Export ("keyboard")]
		bool Keyboard { get; }

		// @property (readonly, nonatomic) BOOL camera;
		[Export ("camera")]
		bool Camera { get; }

		// @property (readonly, nonatomic) BOOL destructive;
		[Export ("destructive")]
		bool Destructive { get; }

		// @property (readonly, nonatomic, strong) ActitoNotificationActionIcon * _Nullable icon;
		[NullAllowed, Export ("icon", ArgumentSemantic.Strong)]
		ActitoNotificationActionIcon Icon { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type label:(NSString * _Nonnull)label target:(NSString * _Nullable)target keyboard:(BOOL)keyboard camera:(BOOL)camera destructive:(BOOL)destructive icon:(ActitoNotificationActionIcon * _Nullable)icon __attribute__((objc_designated_initializer));
		[Export ("initWithType:label:target:keyboard:camera:destructive:icon:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, string label, [NullAllowed] string target, bool keyboard, bool camera, bool destructive, [NullAllowed] ActitoNotificationActionIcon icon);
	}

	// @interface ActitoNotificationActionIcon : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding28ActitoNotificationActionIcon")]
	[DisableDefaultCtor]
	interface ActitoNotificationActionIcon
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable android;
		[NullAllowed, Export ("android")]
		string Android { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable ios;
		[NullAllowed, Export ("ios")]
		string Ios { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable web;
		[NullAllowed, Export ("web")]
		string Web { get; }

		// -(instancetype _Nonnull)initWithAndroid:(NSString * _Nullable)android ios:(NSString * _Nullable)ios web:(NSString * _Nullable)web __attribute__((objc_designated_initializer));
		[Export ("initWithAndroid:ios:web:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string android, [NullAllowed] string ios, [NullAllowed] string web);
	}

	// @interface ActitoNotificationAttachment : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding28ActitoNotificationAttachment")]
	[DisableDefaultCtor]
	interface ActitoNotificationAttachment
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull mimeType;
		[Export ("mimeType")]
		string MimeType { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull uri;
		[Export ("uri")]
		string Uri { get; }

		// -(instancetype _Nonnull)initWithMimeType:(NSString * _Nonnull)mimeType uri:(NSString * _Nonnull)uri __attribute__((objc_designated_initializer));
		[Export ("initWithMimeType:uri:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string mimeType, string uri);
	}

	// @interface ActitoNotificationContent : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding25ActitoNotificationContent")]
	[DisableDefaultCtor]
	interface ActitoNotificationContent
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic) id _Nonnull data;
		[Export ("data")]
		NSObject Data { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type data:(id _Nonnull)data __attribute__((objc_designated_initializer));
		[Export ("initWithType:data:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, NSObject data);
	}

	// @interface ActitoTime : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC13ActitoBinding10ActitoTime")]
	[DisableDefaultCtor]
	interface ActitoTime
	{
		// @property (readonly, nonatomic) NSInteger hours;
		[Export ("hours")]
		nint Hours { get; }

		// @property (readonly, nonatomic) NSInteger minutes;
		[Export ("minutes")]
		nint Minutes { get; }

		// -(instancetype _Nonnull)initWithHours:(NSInteger)hours minutes:(NSInteger)minutes __attribute__((objc_designated_initializer));
		[Export ("initWithHours:minutes:")]
		[DesignatedInitializer]
		NativeHandle Constructor (nint hours, nint minutes);
	}
}

