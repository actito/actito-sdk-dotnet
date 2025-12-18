using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ActitoSdk;

namespace Sample.ViewModels;

public partial class TagsViewModel : ObservableObject
{
    [ObservableProperty] private IList<string> _tags = new List<string>();

    public TagsViewModel()
    {
        FetchTags();
    }

    [RelayCommand]
    private void FetchTags()
    {
        Task.Run(async () =>
        {
            try
            {
                Tags = await Actito.Device.FetchTagsAsync();
                Console.WriteLine("Fetched tags successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to fetch tags: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void AddTags()
    {
        var tags = new List<string> { "react-native", "hpinhal", "remove-me" };

        Task.Run(async () =>
        {
            try
            {
                await Actito.Device.AddTagsAsync(tags);
                FetchTags();

                Console.WriteLine("Added tags successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add tags: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void RemoveTag()
    {
        Task.Run(async () =>
        {
            try
            {
                await Actito.Device.RemoveTagAsync("remove-me");
                FetchTags();

                Console.WriteLine("Remove tag successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to remove tag: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void ClearTags()
    {
        Task.Run(async () =>
        {
            try
            {
                await Actito.Device.ClearTagsAsync();
                FetchTags();

                Console.WriteLine("Cleared tags successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to clear tags: {e.Message}");
            }
        });
    }
}
