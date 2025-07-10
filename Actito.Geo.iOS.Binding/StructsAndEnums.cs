using ObjCRuntime;

namespace ActitoSdk.Geo.iOS.Binding
{
	[Native]
	public enum ActitoBeaconProximity : long
	{
		Unknown = 0,
		Immediate = 1,
		Near = 2,
		Far = 3
	}
}
