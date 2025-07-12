using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Shared.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }

    private FullName(string firstName, string lastName, string? patronymic = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string firstName, string lastName, string? patronymic = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired("FirstName");

        if (firstName.Length > LengthConstants.Length100)
            return Errors.General.ValueIsInvalid("FirstName");

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired("LastName");

        if (lastName.Length > LengthConstants.Length100)
            return Errors.General.ValueIsInvalid("LastName");

        if (patronymic != null && patronymic.Length > LengthConstants.Length100)
            return Errors.General.ValueIsRequired("Patronymic");

        return new FullName(firstName, lastName, patronymic);
    }
}