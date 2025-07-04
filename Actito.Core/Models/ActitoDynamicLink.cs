namespace ActitoSdk.Core.Models;

public class ActitoDynamicLink
{
    public string Target { get; }

    public ActitoDynamicLink(string target)
    {
        Target = target;
    }
}
