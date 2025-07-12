using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record PhoneNumber
{
    private const string PhoneRegexPattern = @"^\+?[0-9\s\-\(\)]{5,20}$";
    private static readonly Regex PhoneRegex = new(
        PhoneRegexPattern,
        RegexOptions.Compiled,
        TimeSpan.FromMilliseconds(100)
    );

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = Regex.Replace(value, @"[^\d+]", "");
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired("PhoneNumber");

        if (!PhoneRegex.IsMatch(value))
            return Errors.General.ValueIsInvalid("PhoneNumber");

        if (value.Length > LengthConstants.Length100)
            return Errors.General.ValueIsInvalid("PhoneNumber");

        return new PhoneNumber(value);
    }
}