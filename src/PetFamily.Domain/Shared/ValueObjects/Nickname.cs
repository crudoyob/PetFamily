using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed record Nickname
{
    public string Value { get; }

    private Nickname(string value)
    {
        Value = value;
    }
    
    public static Result<Nickname, Error> Create(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Errors.General.ValueIsRequired("Nickname");
        
        if (nickname.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Nickname");
        
        return new Nickname(nickname);
    }
}