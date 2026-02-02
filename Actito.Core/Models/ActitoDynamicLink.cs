namespace ActitoSdk.Core.Models;

/// <summary>
/// Represents a dynamic link configuration in Actito.
/// </summary>
/// <remarks>
/// A dynamic link defines a target destination that can be resolved or interpreted, 
/// such as a deep link, in-app route, or external URL.
/// </remarks>
public class ActitoDynamicLink
{
    /// <summary>
    /// The target destination of the dynamic link.
    /// </summary>
    public string Target { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoDynamicLink"/>.
    /// </summary>
    public ActitoDynamicLink(string target)
    {
        Target = target;
    }
}
