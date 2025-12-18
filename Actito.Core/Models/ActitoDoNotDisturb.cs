namespace ActitoSdk.Core.Models;

public class ActitoDoNotDisturb
{
    public ActitoTime Start;
    public ActitoTime End;

    public ActitoDoNotDisturb(ActitoTime start, ActitoTime end)
    {
        Start = start;
        End = end;
    }
}
