using Sample.Pages.AssetsGroup;
using Sample.Pages.Beacons;
using Sample.Pages.CustomEvents;
using Sample.Pages.Device;
using Sample.Pages.Inbox;
using Sample.Pages.Tags;

namespace Sample;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(DevicePage), typeof(DevicePage));
		Routing.RegisterRoute(nameof(InboxPage), typeof(InboxPage));
		Routing.RegisterRoute(nameof(TagsPage), typeof(TagsPage));
		Routing.RegisterRoute(nameof(BeaconsPage), typeof(BeaconsPage));
		Routing.RegisterRoute(nameof(AssetsGroupPage), typeof(AssetsGroupPage));
		Routing.RegisterRoute(nameof(CustomEventsPage), typeof(CustomEventsPage));
	}
}
