using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Email
{
    private const string EmailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";
    private static readonly Regex EmailRegex = new(
        EmailRegexPattern,
        RegexOptions.Compiled | RegexOptions.IgnoreCase,
        TimeSpan.FromMilliseconds(200)
    );

    public string Value { get; }

    private Email(string value)
    {
        Value = value.ToLowerInvariant();
    }

    public static Result<Email, Error> Create(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired("Email");

        if (!EmailRegex.IsMatch(value))
            return Errors.General.ValueIsInvalid("Email");

        if (value.Length > LengthConstants.Length100)
            return Errors.General.ValueIsInvalid("Email");

        return new Email(value);
    }
}