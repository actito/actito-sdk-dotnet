using ActitoSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace ActitoSdk.Scannables.iOS.Binding
{
	// @interface ActitoScannable : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC23ActitoScannablesBinding15ActitoScannable")]
	[DisableDefaultCtor]
	interface ActitoScannable
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull scannableId;
		[Export ("scannableId")]
		string ScannableId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull tag;
		[Export ("tag")]
		string Tag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic, strong) ActitoNotification * _Nullable notification;
		[NullAllowed, Export ("notification", ArgumentSemantic.Strong)]
		ActitoNotification Notification { get; }

		// -(instancetype _Nonnull)initWithScannableId:(NSString * _Nonnull)scannableId name:(NSString * _Nonnull)name tag:(NSString * _Nonnull)tag type:(NSString * _Nonnull)type notification:(ActitoNotification * _Nullable)notification __attribute__((objc_designated_initializer));
		[Export ("initWithScannableId:name:tag:type:notification:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string scannableId, string name, string tag, string type, [NullAllowed] ActitoNotification notification);
	}

	// @interface ActitoScannablesNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoScannablesNativeBinding
	{
		// @property (readonly, nonatomic) BOOL canStartNfcScannableSession;
		[Export ("canStartNfcScannableSession")]
		bool CanStartNfcScannableSession { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoScannablesNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoScannablesNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)startScannableSession:(UIViewController * _Nonnull)controller;
		[Export ("startScannableSession:")]
		void StartScannableSession (UIViewController controller);

		// -(void)startNfcScannableSession;
		[Export ("startNfcScannableSession")]
		void StartNfcScannableSession ();

		// -(void)startQrCodeScannableSession:(UIViewController * _Nonnull)controller modal:(BOOL)modal;
		[Export ("startQrCodeScannableSession:modal:")]
		void StartQrCodeScannableSession (UIViewController controller, bool modal);

		// -(void)fetch:(NSString * _Nonnull)tag :(void (^ _Nonnull)(ActitoScannable * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetch:::")]
		void Fetch (string tag, Action<ActitoScannable> onSuccess, Action<NSError> onFailure);
	}

	// @protocol ActitoScannablesNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP23ActitoScannablesBinding37ActitoScannablesNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP23ActitoScannablesBinding37ActitoScannablesNativeBindingDelegate_")]
	interface ActitoScannablesNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoScannablesNativeBinding * _Nonnull)actitoScannables didInvalidateScannerSession:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("actito:didInvalidateScannerSession:")]
		void DidInvalidateScannerSession (ActitoScannablesNativeBinding actitoScannables, NSError error);

		// @required -(void)actito:(ActitoScannablesNativeBinding * _Nonnull)actitoScannables didDetectScannable:(ActitoScannable * _Nonnull)scannable;
		[Abstract]
		[Export ("actito:didDetectScannable:")]
		void DidDetectScannable (ActitoScannablesNativeBinding actitoScannables, ActitoScannable scannable);
	}
}

