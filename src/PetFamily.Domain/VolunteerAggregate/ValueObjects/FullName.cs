using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects;

public sealed record FullName
{
    public string LastName { get; }
    public string FirstName { get; }
    public string? Patronymic { get; }

    private FullName(string lastName, string firstName, string? patronymic = null)
    {
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string lastName, string firstName, string? patronymic = null)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired("LastName");
        if (lastName.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("LastName");
        
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired("FirstName");
        if (firstName.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("FirstName");

        if (patronymic != null && patronymic.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsRequired("Patronymic");

        return new FullName(firstName, lastName, patronymic);
    }
}