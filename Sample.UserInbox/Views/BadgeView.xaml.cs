namespace Sample.UserInbox.Views;

public partial class BadgeView : ContentView
{
    public static readonly BindableProperty BadgeProperty =
        BindableProperty.Create(nameof(Badge), typeof(int), typeof(BadgeView), 0);

    public BadgeView()
    {
        InitializeComponent();
    }

    public int Badge
    {
        get => (int)GetValue(BadgeProperty);
        set => SetValue(BadgeProperty, value);
    }
}
