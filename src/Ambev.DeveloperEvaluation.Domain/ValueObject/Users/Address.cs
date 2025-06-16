namespace Ambev.DeveloperEvaluation.Domain.ValueObject.Users;

public class Address
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Zipcode { get; set; } = string.Empty;
    public GeoLocation Geolocation { get; set; } = new();

    public Address(string city, string street, int number, string zipcode, GeoLocation geolocation)
    {
        City = city;
        Street = street;
        Number = number;
        Zipcode = zipcode;
        Geolocation = geolocation;
    }

    public Address() { }
}
