namespace ActitoSdk.Core.Models;

/// <summary>
/// Represents time without a date or time zone.
/// </summary>
/// <remarks>
/// <see cref="ActitoTime"/> is used for time-based configurations such as
/// do-not-disturb windows. It stores hours and minutes in 24-hour
/// format and enforces valid ranges.
/// </remarks>
public class ActitoTime
{
    /// <summary>
    /// Hour component of the time in 24-hour format.
    /// </summary>
    /// <remarks>
    /// Must be between `0` and `23` (inclusive).
    /// </remarks>
    public int Hours { get; }

    /// <summary>
    /// Minute component of the time.
    /// </summary>
    /// <remarks>
    /// Must be between `0` and `59` (inclusive).
    /// </remarks>
    public int Minutes { get; }

    /// <summary>
    /// Constructor for <see cref="ActitoNotification"/>.
    /// </summary>
    /// <exception cref="ArgumentException">If <see cref="ActitoTime.Hours"/>
    /// or <see cref="ActitoTime.Minutes"/> are out of range.</exception>
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

    /// <summary>
    /// Returns the time formatted as a `HH:mm` string.
    /// </summary>
    public override string ToString()
    {
        return $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}";
    }

    /// <summary>
    /// Creates an [ActitoTime] from a `HH:mm` formatted string.
    /// </summary>
    /// <remarks>
    /// For example: `"09:30"` or `"18:05"`.
    /// </remarks>
    /// <param name="time">The string containig the time.</param>
    /// <returns>An <see cref="ActitoTime"/> instance.</returns>
    /// <exception cref="ArgumentException">If the string is not in a valid format
    /// or represents an invalid time.</exception>
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
