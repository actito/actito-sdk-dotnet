using ActitoSdk.Inbox.Core.Models;

namespace Sample.Pages.Inbox.Views;

public partial class InboxItemView : ContentView
{
    public static readonly BindableProperty ItemProperty =
        BindableProperty.Create(nameof(Item), typeof(ActitoInboxItem), typeof(InboxItemView));

    public ActitoInboxItem Item
    {
        get => (ActitoInboxItem)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public InboxItemView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
