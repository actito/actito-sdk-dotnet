namespace ActitoSdk.Core.Models;

/// <summary>
/// Defines a do-not-disturb time window for an Actito device.
/// </summary>
/// <remarks>
/// During this period, notifications or communications may be suppressed.
/// </remarks>
public class ActitoDoNotDisturb
{
    /// <summary>
    /// Start time of the do-not-disturb period.
    /// </summary>
    public ActitoTime Start;

    /// <summary>
    /// End time of the do-not-disturb period.
    /// </summary>
    public ActitoTime End;

    /// <summary>
    /// Constructor for <see cref="ActitoDoNotDisturb"/>.
    /// </summary>
    public ActitoDoNotDisturb(ActitoTime start, ActitoTime end)
    {
        Start = start;
        End = end;
    }
}
