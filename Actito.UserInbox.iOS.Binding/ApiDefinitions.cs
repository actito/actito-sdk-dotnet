using ActitoSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.UserInbox.iOS.Binding
{
	// @interface ActitoUserInboxItem : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC22ActitoUserInboxBinding19ActitoUserInboxItem")]
	[DisableDefaultCtor]
	interface ActitoUserInboxItem
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

	// @interface ActitoUserInboxNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoUserInboxNativeBinding
	{
		// -(void)parseResponseFromString:(NSString * _Nonnull)str :(void (^ _Nonnull)(ActitoUserInboxResponse * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("parseResponseFromString:::")]
		void ParseResponseFromString (string str, Action<ActitoUserInboxResponse> onSuccess, Action<NSError> onFailure);

		// -(void)open:(ActitoUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(ActitoNotification * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("open:::")]
		void Open (ActitoUserInboxItem item, Action<ActitoNotification> onSuccess, Action<NSError> onFailure);

		// -(void)markAsRead:(ActitoUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAsRead:::")]
		void MarkAsRead (ActitoUserInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)remove:(ActitoUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("remove:::")]
		void Remove (ActitoUserInboxItem item, Action onSuccess, Action<NSError> onFailure);
	}

	// @interface ActitoUserInboxResponse : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC22ActitoUserInboxBinding23ActitoUserInboxResponse")]
	[DisableDefaultCtor]
	interface ActitoUserInboxResponse
	{
		// @property (readonly, nonatomic) NSInteger count;
		[Export ("count")]
		nint Count { get; }

		// @property (readonly, nonatomic) NSInteger unread;
		[Export ("unread")]
		nint Unread { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoUserInboxItem *> * _Nonnull items;
		[Export ("items", ArgumentSemantic.Copy)]
		ActitoUserInboxItem[] Items { get; }

		// -(instancetype _Nonnull)initWithCount:(NSInteger)count unread:(NSInteger)unread items:(NSArray<ActitoUserInboxItem *> * _Nonnull)items __attribute__((objc_designated_initializer));
		[Export ("initWithCount:unread:items:")]
		[DesignatedInitializer]
		NativeHandle Constructor (nint count, nint unread, ActitoUserInboxItem[] items);
	}
}

