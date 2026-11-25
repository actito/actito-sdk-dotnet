using CommunityToolkit.Mvvm.ComponentModel;
using ActitoSdk.Assets;
using ActitoSdk.Assets.Core.Models;

namespace Sample.ViewModels;

public partial class AssetsGroupViewModel : ObservableObject
{

    [ObservableProperty] private IList<ActitoAsset> _assets = Array.Empty<ActitoAsset>();
    
    public async void FetchAssets(string group)
    {
        try
        {
            Assets = await ActitoAssets.FetchAsync(group);
            Console.WriteLine("Successfully fetched assets.");
        }
        catch (Exception e)
        {
            Assets = Array.Empty<ActitoAsset>();
            Console.WriteLine($"Error fetching assets: {e.Message}.");
        }
    }
}
