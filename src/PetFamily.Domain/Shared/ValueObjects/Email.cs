using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public class Email
{
    public string Value { get; }
    
    private Email(string value)
    {
        Value = value;
    }
    
    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>("Адрес электронной почты не может быть пустым");
        
        return Result.Success(new Email(value));
    }
}