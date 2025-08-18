using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects;

public sealed record Email
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

    public static Result<Email, Error> Create(string input)
    {
        var email = input?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValueIsRequired("Email");

        if (!EmailRegex.IsMatch(email))
            return Errors.General.ValueIsInvalid("Email");

        if (email.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Email");

        return new Email(email);
    }
}