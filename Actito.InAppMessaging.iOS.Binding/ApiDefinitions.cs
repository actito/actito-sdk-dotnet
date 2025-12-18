using System;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.InAppMessaging.iOS.Binding
{
	// @interface ActitoInAppMessage : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC27ActitoInAppMessagingBinding18ActitoInAppMessage")]
	[DisableDefaultCtor]
	interface ActitoInAppMessage
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull inAppMessageId;
		[Export ("inAppMessageId")]
		string InAppMessageId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull context;
		[Export ("context", ArgumentSemantic.Copy)]
		string[] Context { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable message;
		[NullAllowed, Export ("message")]
		string Message { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable image;
		[NullAllowed, Export ("image")]
		string Image { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable landscapeImage;
		[NullAllowed, Export ("landscapeImage")]
		string LandscapeImage { get; }

		// @property (readonly, nonatomic) NSInteger delaySeconds;
		[Export ("delaySeconds")]
		nint DelaySeconds { get; }

		// @property (readonly, nonatomic, strong) ActitoInAppMessageAction * _Nullable primaryAction;
		[NullAllowed, Export ("primaryAction", ArgumentSemantic.Strong)]
		ActitoInAppMessageAction PrimaryAction { get; }

		// @property (readonly, nonatomic, strong) ActitoInAppMessageAction * _Nullable secondaryAction;
		[NullAllowed, Export ("secondaryAction", ArgumentSemantic.Strong)]
		ActitoInAppMessageAction SecondaryAction { get; }

		// -(instancetype _Nonnull)initInAppMessageId:(NSString * _Nonnull)inAppMessageId name:(NSString * _Nonnull)name type:(NSString * _Nonnull)type context:(NSArray<NSString *> * _Nonnull)context title:(NSString * _Nullable)title message:(NSString * _Nullable)message image:(NSString * _Nullable)image landscapeImage:(NSString * _Nullable)landscapeImage delaySeconds:(NSInteger)delaySeconds primaryAction:(ActitoInAppMessageAction * _Nullable)primaryAction secondaryAction:(ActitoInAppMessageAction * _Nullable)secondaryAction __attribute__((objc_designated_initializer));
		[Export ("initInAppMessageId:name:type:context:title:message:image:landscapeImage:delaySeconds:primaryAction:secondaryAction:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string inAppMessageId, string name, string type, string[] context, [NullAllowed] string title, [NullAllowed] string message, [NullAllowed] string image, [NullAllowed] string landscapeImage, nint delaySeconds, [NullAllowed] ActitoInAppMessageAction primaryAction, [NullAllowed] ActitoInAppMessageAction secondaryAction);
	}

	// @interface ActitoInAppMessageAction : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC27ActitoInAppMessagingBinding24ActitoInAppMessageAction")]
	[DisableDefaultCtor]
	interface ActitoInAppMessageAction
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable label;
		[NullAllowed, Export ("label")]
		string Label { get; }

		// @property (readonly, nonatomic) BOOL destructive;
		[Export ("destructive")]
		bool Destructive { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable url;
		[NullAllowed, Export ("url")]
		string Url { get; }

		// -(instancetype _Nonnull)initWithLabel:(NSString * _Nullable)label destructive:(BOOL)destructive url:(NSString * _Nullable)url __attribute__((objc_designated_initializer));
		[Export ("initWithLabel:destructive:url:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string label, bool destructive, [NullAllowed] string url);
	}

	// @interface ActitoInAppMessagingNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoInAppMessagingNativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoInAppMessagingNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoInAppMessagingNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic) BOOL hasMessagesSuppressed;
		[Export ("hasMessagesSuppressed")]
		bool HasMessagesSuppressed { get; set; }

		// -(void)setMessagesSuppressed:(BOOL)suppressed evaluateContext:(BOOL)evaluateContext;
		[Export ("setMessagesSuppressed:evaluateContext:")]
		void SetMessagesSuppressed (bool suppressed, bool evaluateContext);
	}

	// @protocol ActitoInAppMessagingNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP27ActitoInAppMessagingBinding41ActitoInAppMessagingNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP27ActitoInAppMessagingBinding41ActitoInAppMessagingNativeBindingDelegate_")]
	interface ActitoInAppMessagingNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoInAppMessagingNativeBinding * _Nonnull)actito didPresentMessage:(ActitoInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("actito:didPresentMessage:")]
		void DidPresentMessage (ActitoInAppMessagingNativeBinding actito, ActitoInAppMessage message);

		// @required -(void)actito:(ActitoInAppMessagingNativeBinding * _Nonnull)actito didFinishPresentingMessage:(ActitoInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("actito:didFinishPresentingMessage:")]
		void DidFinishPresentingMessage (ActitoInAppMessagingNativeBinding actito, ActitoInAppMessage message);

		// @required -(void)actito:(ActitoInAppMessagingNativeBinding * _Nonnull)actito didFailToPresentMessage:(ActitoInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("actito:didFailToPresentMessage:")]
		void DidFailToPresentMessage (ActitoInAppMessagingNativeBinding actito, ActitoInAppMessage message);

		// @required -(void)actito:(ActitoInAppMessagingNativeBinding * _Nonnull)actito didExecuteAction:(ActitoInAppMessageAction * _Nonnull)action for:(ActitoInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("actito:didExecuteAction:for:")]
		void DidExecuteAction (ActitoInAppMessagingNativeBinding actito, ActitoInAppMessageAction action, ActitoInAppMessage message);

		// @required -(void)actito:(ActitoInAppMessagingNativeBinding * _Nonnull)actito didFailToExecuteAction:(ActitoInAppMessageAction * _Nonnull)action for:(ActitoInAppMessage * _Nonnull)message error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("actito:didFailToExecuteAction:for:error:")]
		void DidFailToExecuteAction (ActitoInAppMessagingNativeBinding actito, ActitoInAppMessageAction action, ActitoInAppMessage message, [NullAllowed] NSError error);
	}
}
