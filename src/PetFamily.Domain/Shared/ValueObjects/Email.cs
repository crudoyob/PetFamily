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

    public static Result<Email> Create(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>("Адрес электронной почты не может быть пустым");

        if (!EmailRegex.IsMatch(value))
            return Result.Failure<Email>("Некорректный формат адреса электронной почты");

        if (value.Length > LengthConstants.Length100)
            return Result.Failure<Email>
                ($"Адрес электронной почты не может превышать {LengthConstants.Length100} символов");

        return Result.Success(new Email(value));
    }
}