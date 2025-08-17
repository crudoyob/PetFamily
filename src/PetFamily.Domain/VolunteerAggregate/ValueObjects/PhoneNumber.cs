using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects;

public sealed record PhoneNumber
{
    
    private const string PhoneRegexPattern = @"^\+\d{1,15}$";
    private static readonly Regex PhoneRegex = new(
        PhoneRegexPattern,
        RegexOptions.Compiled,
        TimeSpan.FromMilliseconds(100)
    );

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        var phoneNumber = input.Trim();

        if (!PhoneRegex.IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid("PhoneNumber");
        
        return new PhoneNumber(phoneNumber);
    }
}