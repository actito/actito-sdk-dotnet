namespace ActitoSdk.Geo.Core.Models;

public class ActitoVisit
{
    public DateTime DepartureDate { get; }
    public DateTime ArrivalDate { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public ActitoVisit(DateTime departureDate, DateTime arrivalDate, double latitude, double longitude)
    {
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Latitude = latitude;
        Longitude = longitude;
    }
}
