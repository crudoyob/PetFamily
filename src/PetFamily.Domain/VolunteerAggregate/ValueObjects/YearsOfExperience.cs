using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects;

public sealed record YearsOfExperience
{
    public int Years { get; }
    public bool IsVerified { get; }
    
    private YearsOfExperience(int years, bool isVerified = false)
    {
        Years = years;
        IsVerified = isVerified;
    }

    public static Result<YearsOfExperience, Error> Create(int years = 0, bool isVerified = false)
    {
        if (years < LengthConstants.LENGTH0)
            return Errors.General.ValueIsInvalid("YearsOfExperience");

        if (years > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("YearsOfExperience");

        return new YearsOfExperience(years, isVerified);
    }

    public static implicit operator int(YearsOfExperience years) => years.Years;

    public override string ToString() => Years.ToString();
}