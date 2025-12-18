using System;
using Foundation;
using UserNotifications;

namespace ActitoSdk.NotificationServiceExtension.iOS.Binding
{
	// @interface ActitoNotificationServiceExtensionNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoNotificationServiceExtensionNativeBinding
	{
		// +(void)handleNotificationRequest:(UNNotificationRequest * _Nonnull)request :(void (^ _Nonnull)(UNNotificationContent * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Static]
		[Export ("handleNotificationRequest:::")]
		void HandleNotificationRequest (UNNotificationRequest request, Action<UNNotificationContent> onSuccess, Action<NSError> onFailure);
	}
}
