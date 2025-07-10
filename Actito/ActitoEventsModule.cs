using ActitoSdk.Core.Internal;

namespace ActitoSdk;

public class ActitoEventsModule
{
    private readonly IActitoPlatform _platform;

    internal ActitoEventsModule(IActitoPlatform platform)
    {
        _platform = platform;
    }

    /// <summary>
    /// Logs in Actito a custom event in the application.
    ///
    /// This function allows logging, in Actito, of application-specific events, optionally associating structured
    /// data for more detailed event tracking and analysis.
    /// </summary>
    /// <param name="eventName">The name of the custom event to log.</param>
    /// <param name="data">Optional structured event data for further details.</param>
    /// <returns>
    /// A task that resolves when the custom event has been successfully logged.
    /// </returns>
    public Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null) =>
        _platform.LogCustomAsync(eventName, data);
}
