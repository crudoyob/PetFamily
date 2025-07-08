using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record HelpRequisite
{
    public string Name { get; }
    public string Description { get; }

    private HelpRequisite(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<HelpRequisite> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<HelpRequisite>("Имя реквизита для помощи не может быть пустым");

        if (name.Length > LengthConstants.Length100)
            return Result.Failure<HelpRequisite>
                ($"Имя реквизита не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<HelpRequisite>("Описание реквизита для помощи не может быть пустым");

        if (description.Length > LengthConstants.Length250)
            return Result.Failure<HelpRequisite>
                ($"Описание реквизита не может превышать {LengthConstants.Length250} символов");

        return Result.Success(new HelpRequisite(name, description));
    }
}