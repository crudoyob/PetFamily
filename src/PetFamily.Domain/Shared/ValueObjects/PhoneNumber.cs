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

    public static Result<PhoneNumber> Create(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<PhoneNumber>("Номер телефона не может быть пустым");

        if (!PhoneRegex.IsMatch(value))
            return Result.Failure<PhoneNumber>("Некорректный формат номера телефона");

        if (value.Length > LengthConstants.Length100)
            return Result.Failure<PhoneNumber>
                ($"Номер телефона не может превышать {LengthConstants.Length100} символов");

        return Result.Success(new PhoneNumber(value));
    }
}