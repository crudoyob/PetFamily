using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed record HelpRequisite
{
    public string Name { get; }
    public string Description { get; }

    private HelpRequisite(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<HelpRequisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.Errors.General.ValueIsRequired("Name");

        if (name.Length > LengthConstants.LENGTH100)
            return Errors.Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.Errors.General.ValueIsRequired("Description");

        if (description.Length > LengthConstants.LENGTH250)
            return Errors.Errors.General.ValueIsInvalid("Description");

        return new HelpRequisite(name, description);
    }
}