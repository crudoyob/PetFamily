using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public record HelpStatus
{
    public static HelpStatus NeedsHelp { get; } = new HelpStatus("Нуждается в помощи");
    public static HelpStatus LookingForHome { get; } = new HelpStatus("Ищет дом");
    public static HelpStatus FoundHome { get; } = new HelpStatus("Нашел дом");

    public string Value { get; }
    
    private HelpStatus(string value)
    {
        Value = value;
    }
    
    public override string ToString() => Value;

    public static Result<HelpStatus> Create(string value)
    {
        return value switch
        {
            "Нуждается в помощи" => Result.Success(NeedsHelp),
            "Ищет дом" => Result.Success(LookingForHome),
            "Нашел дом" => Result.Success(FoundHome),
            _ => Result.Failure<HelpStatus>($"Некорректный статус помощи питомца: {value}")
        };
    }
}