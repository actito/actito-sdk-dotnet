using Sample.ViewModels;

namespace Sample.Pages.Home.Views;

public partial class DoNotDisturbCardView : ContentView
{
    public DoNotDisturbCardView()
    {
        InitializeComponent();
    }

    private void OnDoNotDisturbSwitchToggled(object sender, ToggledEventArgs e)
    {
        var viewModel = (DoNotDisturbViewModel)BindingContext;

        if (e.Value)
        {
            viewModel.UpdateDoNotDisturb();
            return;
        }
        
        viewModel.ClearDoNotDisturb();
    }
}
