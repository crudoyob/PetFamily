using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public class Email
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
        value = value?.Trim();
        
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>("Адрес электронной почты не может быть пустым");
        
        if (!EmailRegex.IsMatch(value))
            return Result.Failure<Email>("Некорректный формат адреса электронной почты");
        
        return Result.Success(new Email(value));
    }
}