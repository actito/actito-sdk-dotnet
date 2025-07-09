namespace Sample.Views;

public partial class BadgeView : ContentView
{
    public static readonly BindableProperty BadgeProperty =
        BindableProperty.Create(nameof(Badge), typeof(int), typeof(KeyValueRowView), 0);

    public int Badge
    {
        get => (int)GetValue(BadgeProperty);
        set => SetValue(BadgeProperty, value);
    }

    public BadgeView()
    {
        InitializeComponent();
    }
}
