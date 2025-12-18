using ObjCRuntime;

namespace ActitoSdk.Push.iOS.Binding
{
	[Native]
	public enum ActitoNotificationDeliveryMechanism : long
	{
		Standard = 0,
		Silent = 1,
		Unknown = 2
	}

	[Native]
	public enum ActitoTransport : long
	{
		Notificare = 0,
		Apns = 1,
		Unknown = 2
	}
}
