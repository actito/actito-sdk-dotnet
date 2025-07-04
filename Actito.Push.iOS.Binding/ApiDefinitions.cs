using ActitoSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace ActitoSdk.Push.iOS.Binding
{
	// @interface ActitoPushNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoPushNativeBinding
	{
		// @property (nonatomic) UNAuthorizationOptions authorizationOptions;
		[Export ("authorizationOptions", ArgumentSemantic.Assign)]
		UNAuthorizationOptions AuthorizationOptions { get; set; }

		// @property (nonatomic) UNNotificationCategoryOptions categoryOptions;
		[Export ("categoryOptions", ArgumentSemantic.Assign)]
		UNNotificationCategoryOptions CategoryOptions { get; set; }

		// @property (nonatomic) UNNotificationPresentationOptions presentationOptions;
		[Export ("presentationOptions", ArgumentSemantic.Assign)]
		UNNotificationPresentationOptions PresentationOptions { get; set; }

		// @property (readonly, nonatomic) BOOL hasRemoteNotificationsEnabled;
		[Export ("hasRemoteNotificationsEnabled")]
		bool HasRemoteNotificationsEnabled { get; }

		// @property (readonly, nonatomic) enum ActitoTransport transport;
		[Export ("transport")]
		ActitoTransport Transport { get; }

		// @property (readonly, nonatomic, strong) ActitoPushSubscription * _Nullable subscription;
		[NullAllowed, Export ("subscription", ArgumentSemantic.Strong)]
		ActitoPushSubscription Subscription { get; }

		// @property (readonly, nonatomic) BOOL allowedUI;
		[Export ("allowedUI")]
		bool AllowedUI { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoPushNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoPushNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)enableRemoteNotifications:(void (^ _Nonnull)(BOOL))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("enableRemoteNotifications::")]
		void EnableRemoteNotifications (Action<bool> onSuccess, Action<NSError> onFailure);

		// -(void)disableRemoteNotifications:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("disableRemoteNotifications::")]
		void DisableRemoteNotifications (Action onSuccess, Action<NSError> onFailure);

		// -(void)registeredForRemoteNotifications:(UIApplication * _Nonnull)application :(NSData * _Nonnull)token;
		[Export ("registeredForRemoteNotifications::")]
		void RegisteredForRemoteNotifications (UIApplication application, NSData token);

		// -(void)failedToRegisterForRemoteNotifications:(UIApplication * _Nonnull)application :(NSError * _Nonnull)error;
		[Export ("failedToRegisterForRemoteNotifications::")]
		void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error);

		// -(void)didReceiveRemoteNotification:(UIApplication * _Nonnull)application :(NSDictionary * _Nonnull)userInfo :(void (^ _Nonnull)(UIBackgroundFetchResult))completionHandler;
		[Export ("didReceiveRemoteNotification:::")]
		void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler);

		// -(void)willPresentNotification:(UNUserNotificationCenter * _Nonnull)center :(UNNotification * _Nonnull)notification :(void (^ _Nonnull)(UNNotificationPresentationOptions))completionHandler;
		[Export ("willPresentNotification:::")]
		void WillPresentNotification (UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler);

		// -(void)didReceiveNotificationResponse:(UNUserNotificationCenter * _Nonnull)center :(UNNotificationResponse * _Nonnull)response :(void (^ _Nonnull)(void))completionHandler;
		[Export ("didReceiveNotificationResponse:::")]
		void DidReceiveNotificationResponse (UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler);

		// -(void)openSettings:(UNUserNotificationCenter * _Nonnull)center :(UNNotification * _Nullable)notification;
		[Export ("openSettings::")]
		void OpenSettings (UNUserNotificationCenter center, [NullAllowed] UNNotification notification);
	}

	// @protocol ActitoPushNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP17ActitoPushBinding31ActitoPushNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP17ActitoPushBinding31ActitoPushNativeBindingDelegate_")]
	interface ActitoPushNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didChangeSubscription:(ActitoPushSubscription * _Nullable)subscription;
		[Abstract]
		[Export ("actito:didChangeSubscription:")]
		void DidChangeSubscription (ActitoPushNativeBinding actitoPush, [NullAllowed] ActitoPushSubscription subscription);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didChangeNotificationSettings:(BOOL)allowedUI;
		[Abstract]
		[Export ("actito:didChangeNotificationSettings:")]
		void DidChangeNotificationSettings (ActitoPushNativeBinding actitoPush, bool allowedUI);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didReceiveUnknownNotification:(NSDictionary * _Nonnull)userInfo;
		[Abstract]
		[Export ("actito:didReceiveUnknownNotification:")]
		void DidReceiveUnknownNotification (ActitoPushNativeBinding actitoPush, NSDictionary userInfo);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didReceiveNotification:(ActitoNotification * _Nonnull)notification deliveryMechanism:(enum ActitoNotificationDeliveryMechanism)deliveryMechanism;
		[Abstract]
		[Export ("actito:didReceiveNotification:deliveryMechanism:")]
		void DidReceiveNotification (ActitoPushNativeBinding actitoPush, ActitoNotification notification, ActitoNotificationDeliveryMechanism deliveryMechanism);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didReceiveSystemNotification:(ActitoSystemNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didReceiveSystemNotification:")]
		void DidReceiveSystemNotification (ActitoPushNativeBinding actitoPush, ActitoSystemNotification notification);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush shouldOpenSettings:(ActitoNotification * _Nullable)notification;
		[Abstract]
		[Export ("actito:shouldOpenSettings:")]
		void ShouldOpenSettings (ActitoPushNativeBinding actitoPush, [NullAllowed] ActitoNotification notification);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didOpenNotification:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didOpenNotification:")]
		void DidOpenNotification (ActitoPushNativeBinding actitoPush, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didOpenUnknownNotification:(NSDictionary * _Nonnull)userInfo;
		[Abstract]
		[Export ("actito:didOpenUnknownNotification:")]
		void DidOpenUnknownNotification (ActitoPushNativeBinding actitoPush, NSDictionary userInfo);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didOpenAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didOpenAction:for:")]
		void DidOpenAction (ActitoPushNativeBinding actitoPush, ActitoNotificationAction action, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushNativeBinding * _Nonnull)actitoPush didOpenUnknownAction:(NSString * _Nonnull)action for:(NSDictionary * _Nonnull)notification responseText:(NSString * _Nullable)responseText;
		[Abstract]
		[Export ("actito:didOpenUnknownAction:for:responseText:")]
		void DidOpenUnknownAction (ActitoPushNativeBinding actitoPush, string action, NSDictionary notification, [NullAllowed] string responseText);
	}

	// @interface ActitoPushSubscription : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC17ActitoPushBinding22ActitoPushSubscription")]
	[DisableDefaultCtor]
	interface ActitoPushSubscription
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull token;
		[Export ("token")]
		string Token { get; }

		// -(instancetype _Nonnull)initWithToken:(NSString * _Nonnull)token __attribute__((objc_designated_initializer));
		[Export ("initWithToken:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string token);
	}

	// @interface ActitoSystemNotification : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC17ActitoPushBinding24ActitoSystemNotification")]
	[DisableDefaultCtor]
	interface ActitoSystemNotification
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
		[Export ("id")]
		string Id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull extra;
		[Export ("extra", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Extra { get; }

		// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id type:(NSString * _Nonnull)type extra:(NSDictionary<NSString *,id> * _Nonnull)extra __attribute__((objc_designated_initializer));
		[Export ("initWithId:type:extra:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string id, string type, NSDictionary<NSString, NSObject> extra);
	}
}

