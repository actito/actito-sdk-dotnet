using Sample.UserInbox.ViewModels;

namespace Sample.UserInbox.Pages.Home;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        var viewModel = (HomeViewModel)BindingContext;
        viewModel.RefreshBadge();
    }
}
