using ActitoSdk.iOS.Binding;

using Foundation;
using ObjCRuntime;
using UIKit;

namespace ActitoSdk.Push.UI.iOS.Binding
{
	// @interface ActitoPushUINativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoPushUINativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoPushUINativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoPushUINativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)presentNotification:(ActitoNotification * _Nonnull)notification in:(UIViewController * _Nonnull)controller;
		[Export ("presentNotification:in:")]
		void PresentNotification (ActitoNotification notification, UIViewController controller);

		// -(void)presentAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification in:(UIViewController * _Nonnull)controller;
		[Export ("presentAction:for:in:")]
		void PresentAction (ActitoNotificationAction action, ActitoNotification notification, UIViewController controller);

		// -(BOOL)requiresViewController:(ActitoNotification * _Nonnull)notification __attribute__((warn_unused_result("")));
		[Export ("requiresViewController:")]
		bool RequiresViewController (ActitoNotification notification);
	}

	// @protocol ActitoPushUINativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP19ActitoPushUIBinding33ActitoPushUINativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP19ActitoPushUIBinding33ActitoPushUINativeBindingDelegate_")]
	interface ActitoPushUINativeBindingDelegate
	{
		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI willPresentNotification:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:willPresentNotification:")]
		void WillPresentNotification (ActitoPushUINativeBinding actitoPushUI, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didPresentNotification:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didPresentNotification:")]
		void DidPresentNotification (ActitoPushUINativeBinding actitoPushUI, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didFinishPresentingNotification:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didFinishPresentingNotification:")]
		void DidFinishPresentingNotification (ActitoPushUINativeBinding actitoPushUI, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didFailToPresentNotification:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didFailToPresentNotification:")]
		void DidFailToPresentNotification (ActitoPushUINativeBinding actitoPushUI, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didClickURL:(NSURL * _Nonnull)url in:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didClickURL:in:")]
		void DidClickURL (ActitoPushUINativeBinding actitoPushUI, NSUrl url, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI willExecuteAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:willExecuteAction:for:")]
		void WillExecuteAction (ActitoPushUINativeBinding actitoPushUI, ActitoNotificationAction action, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didExecuteAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didExecuteAction:for:")]
		void DidExecuteAction (ActitoPushUINativeBinding actitoPushUI, ActitoNotificationAction action, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didNotExecuteAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didNotExecuteAction:for:")]
		void DidNotExecuteAction (ActitoPushUINativeBinding actitoPushUI, ActitoNotificationAction action, ActitoNotification notification);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didFailToExecuteAction:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("actito:didFailToExecuteAction:for:error:")]
		void DidFailToExecuteAction (ActitoPushUINativeBinding actitoPushUI, ActitoNotificationAction action, ActitoNotification notification, [NullAllowed] NSError error);

		// @required -(void)actito:(ActitoPushUINativeBinding * _Nonnull)actitoPushUI didReceiveCustomAction:(NSURL * _Nonnull)url in:(ActitoNotificationAction * _Nonnull)action for:(ActitoNotification * _Nonnull)notification;
		[Abstract]
		[Export ("actito:didReceiveCustomAction:in:for:")]
		void DidReceiveCustomAction (ActitoPushUINativeBinding actitoPushUI, NSUrl url, ActitoNotificationAction action, ActitoNotification notification);
	}
}

