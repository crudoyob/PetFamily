using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record PhoneNumber
{
    public string Value { get; }
    
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<PhoneNumber>("Номер телефона не может быть пустым");
        
        return Result.Success(new PhoneNumber(value));
    }
}