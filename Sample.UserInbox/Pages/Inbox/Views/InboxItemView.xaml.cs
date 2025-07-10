using ActitoSdk.UserInbox.Core.Models;

namespace Sample.UserInbox.Pages.Inbox.Views;

public partial class InboxItemView : ContentView
{
    public static readonly BindableProperty ItemProperty =
        BindableProperty.Create(nameof(Item), typeof(ActitoUserInboxItem), typeof(InboxItemView));

    public InboxItemView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ActitoUserInboxItem Item
    {
        get => (ActitoUserInboxItem)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }
}
