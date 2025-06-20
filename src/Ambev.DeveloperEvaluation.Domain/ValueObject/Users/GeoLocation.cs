namespace Ambev.DeveloperEvaluation.Domain.ValueObject.Users;

public class GeoLocation
{
    public string Lat { get; set; } = string.Empty;
    public string Long { get; set; } = string.Empty;

    public GeoLocation(string lat, string @long)
    {
        Lat = lat;
        Long = @long;
    }

    public GeoLocation() { }
}