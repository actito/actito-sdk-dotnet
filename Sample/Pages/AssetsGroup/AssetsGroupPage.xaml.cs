using Sample.ViewModels;

namespace Sample.Pages.AssetsGroup;

public partial class AssetsGroupPage : ContentPage
{
    public AssetsGroupPage()
    {
        InitializeComponent();
    }

    private void OnFetchButtonClicked(object sender, EventArgs e)
    {
        var assetsGroup = AssetsGroupInputText.Text;
        
        if (string.IsNullOrWhiteSpace(assetsGroup))
        {
            MainThread.InvokeOnMainThreadAsync( async () =>
            {
                await DisplayAlert("Error", "Please enter assets group", "OK");
            });
            
            return;
        }
        
        AssetsGroupInputText.Text = string.Empty;
        
        var viewModel = (AssetsGroupViewModel)BindingContext;
        viewModel.FetchAssets(assetsGroup);
    }
}
