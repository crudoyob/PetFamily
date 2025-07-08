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

    public static Result<FullName> Create(string firstName, string lastName, string? patronymic = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FullName>("Имя человека не может быть пустым");

        if (firstName.Length > LengthConstants.Length100)
            return Result.Failure<FullName>
                ($"Имя человека не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<FullName>("Фамилия человека не может быть пустой");

        if (lastName.Length > LengthConstants.Length100)
            return Result.Failure<FullName>
                ($"Фамилия человека не может превышать {LengthConstants.Length100} символов");

        if (patronymic != null && patronymic.Length > LengthConstants.Length100)
            return Result.Failure<FullName>
                ($"Отчество человека не может превышать {LengthConstants.Length100} символов");

        return Result.Success(new FullName(firstName, lastName, patronymic));
    }
}