using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public sealed record HelpStatus
{
    public static HelpStatus NeedsHelp { get; } = new("Нуждается в помощи");
    public static HelpStatus LookingForHome { get; } = new("Ищет дом");
    public static HelpStatus FoundHome { get; } = new("Нашел дом");

    public string Value { get; }

    private HelpStatus(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static Result<HelpStatus, Error> Create(string value)
    {
        return value switch
        {
            "Нуждается в помощи" => NeedsHelp,
            "Ищет дом" => LookingForHome,
            "Нашел дом" => FoundHome,
            _ => Errors.General.ValueIsInvalid("HelpStatus")
        };
    }
}