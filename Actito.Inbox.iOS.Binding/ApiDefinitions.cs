using ActitoSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.Inbox.iOS.Binding
{
	// @interface ActitoInboxItem : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC18ActitoInboxBinding15ActitoInboxItem")]
	[DisableDefaultCtor]
	interface ActitoInboxItem
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull inboxItemId;
		[Export ("inboxItemId")]
		string InboxItemId { get; }

		// @property (readonly, nonatomic, strong) ActitoNotification * _Nonnull notification;
		[Export ("notification", ArgumentSemantic.Strong)]
		ActitoNotification Notification { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull time;
		[Export ("time", ArgumentSemantic.Copy)]
		NSDate Time { get; }

		// @property (readonly, nonatomic) BOOL opened;
		[Export ("opened")]
		bool Opened { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nullable expires;
		[NullAllowed, Export ("expires", ArgumentSemantic.Copy)]
		NSDate Expires { get; }

		// -(instancetype _Nonnull)initWithInboxItemId:(NSString * _Nonnull)inboxItemId notification:(ActitoNotification * _Nonnull)notification time:(NSDate * _Nonnull)time opened:(BOOL)opened expires:(NSDate * _Nullable)expires __attribute__((objc_designated_initializer));
		[Export ("initWithInboxItemId:notification:time:opened:expires:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string inboxItemId, ActitoNotification notification, NSDate time, bool opened, [NullAllowed] NSDate expires);
	}

	// @interface ActitoInboxNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoInboxNativeBinding
	{
		// @property (readonly, copy, nonatomic) NSArray<ActitoInboxItem *> * _Nonnull items;
		[Export ("items", ArgumentSemantic.Copy)]
		ActitoInboxItem[] Items { get; }

		// @property (readonly, nonatomic) NSInteger badge;
		[Export ("badge")]
		nint Badge { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoInboxNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoInboxNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)refresh;
		[Export ("refresh")]
		void Refresh ();

		// -(void)refreshBadge:(void (^ _Nonnull)(NSInteger))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("refreshBadge::")]
		void RefreshBadge (Action<nint> onSuccess, Action<NSError> onFailure);

		// -(void)open:(ActitoInboxItem * _Nonnull)item :(void (^ _Nonnull)(ActitoNotification * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("open:::")]
		void Open (ActitoInboxItem item, Action<ActitoNotification> onSuccess, Action<NSError> onFailure);

		// -(void)markAsRead:(ActitoInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAsRead:::")]
		void MarkAsRead (ActitoInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)markAllAsRead:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAllAsRead::")]
		void MarkAllAsRead (Action onSuccess, Action<NSError> onFailure);

		// -(void)remove:(ActitoInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("remove:::")]
		void Remove (ActitoInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)clear:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("clear::")]
		void Clear (Action onSuccess, Action<NSError> onFailure);
	}

	// @protocol ActitoInboxNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP18ActitoInboxBinding32ActitoInboxNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP18ActitoInboxBinding32ActitoInboxNativeBindingDelegate_")]
	interface ActitoInboxNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoInboxNativeBinding * _Nonnull)actitoInbox didUpdateInbox:(NSArray<ActitoInboxItem *> * _Nonnull)items;
		[Abstract]
		[Export ("actito:didUpdateInbox:")]
		void DidUpdateInbox (ActitoInboxNativeBinding actitoInbox, ActitoInboxItem[] items);

		// @required -(void)actito:(ActitoInboxNativeBinding * _Nonnull)actitoInbox didUpdateBadge:(NSInteger)badge;
		[Abstract]
		[Export ("actito:didUpdateBadge:")]
		void DidUpdateBadge (ActitoInboxNativeBinding actitoInbox, nint badge);
	}
}

