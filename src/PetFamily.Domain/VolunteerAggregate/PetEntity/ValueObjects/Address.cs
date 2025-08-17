using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public sealed record Address
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

    private Address(
        string country,
        string region,
        string city,
        string? district,
        string street,
        string building,
        string? letter,
        string? corpus,
        string? construction,
        string? apartment,
        string? postalCode)
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

    public static Result<Address, Error> Create(
        string country,
        string region,
        string city,
        string street,
        string building,
        string? district = null,
        string? letter = null,
        string? corpus = null,
        string? construction = null,
        string? apartment = null,
        string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsRequired("Country");
        if (country.Length > LengthConstants.LENGTH50)
            return Errors.General.ValueIsInvalid("Country");

        if (string.IsNullOrWhiteSpace(region))
            return Errors.General.ValueIsRequired("Region");
        if (region.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Region");

        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired("City");
        if (city.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("City");

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired("Street");
        if (street.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Street");

        if (string.IsNullOrWhiteSpace(building))
            return Errors.General.ValueIsRequired("Building");
        if (building.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Building");

        if (district != null && district.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("District");

        if (letter != null && letter.Length > LengthConstants.LENGTH1)
            return Errors.General.ValueIsInvalid("Letter");

        if (corpus != null && corpus.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Corpus");

        if (construction != null && construction.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Construction");

        if (apartment != null && apartment.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Apartment");

        if (postalCode != null && postalCode.Length > LengthConstants.LENGTH6)
            return Errors.General.ValueIsInvalid("PostalCode");

        return new Address(
            country,
            region,
            city,
            district,
            street,
            building,
            letter,
            corpus,
            construction,
            apartment,
            postalCode);
    }
}