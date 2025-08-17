using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }
    
    public static Result<Description, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");
        
        if (description.Length > LengthConstants.LENGTH1500)
            return Errors.General.ValueIsInvalid("Description");
        
        return new Description(description);
    }
}