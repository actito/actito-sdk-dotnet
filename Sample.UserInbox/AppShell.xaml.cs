using Sample.UserInbox.Pages.Inbox;

namespace Sample.UserInbox;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(InboxPage), typeof(InboxPage));
    }
}
