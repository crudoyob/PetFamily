using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects;

public sealed record YearsOfExperience
{
    public int Value { get; }
    public bool IsVerified { get; }

    private YearsOfExperience(int value, bool isVerified = false)
    {
        Value = value;
        IsVerified = isVerified;
    }

    public static Result<YearsOfExperience, Error> Create(int yearsOfExperience = 0, bool isVerified = false)
    {
        if (yearsOfExperience < LengthConstants.LENGTH0)
            return Errors.General.ValueIsInvalid("YearsOfExperience");

        if (yearsOfExperience > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("YearsOfExperience");

        return new YearsOfExperience(yearsOfExperience, isVerified);
    }

    public static implicit operator int(YearsOfExperience years) => years.Value;

    public override string ToString() => Value.ToString();
}