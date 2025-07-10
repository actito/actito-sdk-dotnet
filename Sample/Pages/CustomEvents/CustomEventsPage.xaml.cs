using ActitoSdk;

namespace Sample.Pages.CustomEvents;

public partial class CustomEventsPage : ContentPage
{
    public CustomEventsPage()
    {
        InitializeComponent();
    }

    private void RegisterCustomEvent(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                await Actito.Events.LogCustomAsync("dotnet-event");
                Console.WriteLine("Custom event dotnet-event registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering custom event: {ex.Message}");
            }
        });
    }

    private void RegisterCustomEventWithData(object sender, EventArgs e)
    {
        var data = new Dictionary<string, object>
        {
            { "data_key", "data_value" }
        };

        Task.Run(async () =>
        { 
            try
            {
                await Actito.Events.LogCustomAsync("dotnet-event-data", data);
                Console.WriteLine("Custom event dotnet-event-data registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering custom event with data: {ex.Message}");
            }
        });
    }
}
