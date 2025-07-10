using System.Windows.Input;

namespace Sample.UserInbox.Pages.Home.Views;

public partial class AuthenticationFlowCardView : ContentView
{
    public static readonly BindableProperty IsLoggedInProperty =
        BindableProperty.Create(nameof(IsLoggedIn), typeof(bool), typeof(AuthenticationFlowCardView), false);

    public static readonly BindableProperty LoginCommandProperty =
        BindableProperty.Create(nameof(LoginCommand), typeof(ICommand), typeof(AuthenticationFlowCardView));

    public static readonly BindableProperty LogoutCommandProperty =
        BindableProperty.Create(nameof(LogoutCommand), typeof(ICommand), typeof(AuthenticationFlowCardView));

    public AuthenticationFlowCardView()
    {
        InitializeComponent();
    }

    public bool IsLoggedIn
    {
        get => (bool)GetValue(IsLoggedInProperty);
        set => SetValue(IsLoggedInProperty, value);
    }

    public ICommand LoginCommand
    {
        get => (ICommand)GetValue(LoginCommandProperty);
        set => SetValue(LoginCommandProperty, value);
    }

    public ICommand LogoutCommand
    {
        get => (ICommand)GetValue(LogoutCommandProperty);
        set => SetValue(LogoutCommandProperty, value);
    }
}

