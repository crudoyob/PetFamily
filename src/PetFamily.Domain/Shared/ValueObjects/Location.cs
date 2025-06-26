using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Location
{
    public string Country { get; }
    public string Region { get; }
    public string City { get; }
    public string? District { get; }
    public string Street { get; }
    public string Building { get; }
    public string? Litera { get; }
    public string? Corpus { get; }
    public string? Construction { get; }
    public string? Apartment { get; }
    public string? PostalCode { get; }
    
    private Location(string country, string region, string city, string? district, string street, string building,
        string? litera, string? corpus, string? construction, string? apartment, string? postalCode)
    {
        Country = country;
        Region = region;
        City = city;
        District = district;
        Street = street;
        Building = building;
        Litera = litera;
        Corpus = corpus;
        Construction = construction;
        Apartment = apartment;
        PostalCode = postalCode;
    }
    
    public static Result<Location> Create(string country, string region, string city, string street, string building, 
        string? district = null, string? litera = null, string? corpus = null, string? construction = null, 
        string? apartment = null, string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Result.Failure<Location>("Страна не может быть пустой");
        
        if (string.IsNullOrWhiteSpace(region))
            return Result.Failure<Location>("Регион не может быть пустым");
    
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Location>("Город не может быть пустым");
    
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Location>("Улица не может быть пустой");
    
        if (string.IsNullOrWhiteSpace(building))
            return Result.Failure<Location>("Номер дома не может быть пустым");

        return Result.Success(new Location(country, region, city, district, street, building, litera, corpus, construction, 
            apartment, postalCode));
    }
}
