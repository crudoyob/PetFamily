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
    public string? Letter { get; }
    public string? Corpus { get; }
    public string? Construction { get; }
    public string? Apartment { get; }
    public string? PostalCode { get; }

    private Location(string country, string region, string city, string? district, string street, string building,
        string? letter, string? corpus, string? construction, string? apartment, string? postalCode)
    {
        Country = country;
        Region = region;
        City = city;
        District = district;
        Street = street;
        Building = building;
        Letter = letter;
        Corpus = corpus;
        Construction = construction;
        Apartment = apartment;
        PostalCode = postalCode;
    }

    public static Result<Location> Create(string country, string region, string city, string street, string building,
        string? district = null, string? letter = null, string? corpus = null, string? construction = null,
        string? apartment = null, string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Result.Failure<Location>("Страна не может быть пустой");
        if (country.Length > LengthConstants.Length50)
            return Result.Failure<Location>($"Страна не может превышать {LengthConstants.Length50} символов");

        if (string.IsNullOrWhiteSpace(region))
            return Result.Failure<Location>("Регион не может быть пустым");
        if (region.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Регион не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Location>("Город не может быть пустым");
        if (city.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Город не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Location>("Улица не может быть пустой");
        if (street.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Улица не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(building))
            return Result.Failure<Location>("Номер дома не может быть пустым");
        if (building.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Номер дома не может превышать {LengthConstants.Length100} символов");

        if (district != null && district.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Район не может превышать {LengthConstants.Length100} символов");

        if (letter != null && letter.Length > LengthConstants.Length1)
            return Result.Failure<Location>($"Литера не может превышать {LengthConstants.Length1} символ");

        if (corpus != null && corpus.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Корпус не может превышать {LengthConstants.Length100} символов");

        if (construction != null && construction.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Строение не может превышать {LengthConstants.Length100} символов");

        if (apartment != null && apartment.Length > LengthConstants.Length100)
            return Result.Failure<Location>($"Квартира не может превышать {LengthConstants.Length100} символов");

        if (postalCode != null && postalCode.Length > LengthConstants.Length6)
            return Result.Failure<Location>
                ($"Почтовый индекс не может превышать {LengthConstants.Length6} символов");

        return Result.Success(new Location(country, region, city, district, street, building, letter, corpus, 
            construction, apartment, postalCode));
    }
}