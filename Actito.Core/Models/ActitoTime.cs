namespace ActitoSdk.Core.Models;

public class ActitoTime
{
    public int Hours { get; }

    public int Minutes { get; }

    public ActitoTime(int hours, int minutes)
    {
        if (hours < 0 || hours > 23)
        {
            throw new ArgumentException("Invalid time", nameof(hours));
        }

        if (minutes < 0 || minutes > 59)
        {
            throw new ArgumentException("Invalid time", nameof(minutes));
        }

        Hours = hours;
        Minutes = minutes;
    }

    public override string ToString()
    {
        return $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}";
    }

    public static ActitoTime FromString(string time)
    {
        var parts = time.Split(":");
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid time string.", nameof(time));
        }

        try
        {
            var hours = int.Parse(parts[0]);
            var minutes = int.Parse(parts[1]);

            return new ActitoTime(hours, minutes);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid time string.", nameof(time));
        }
    }
}
