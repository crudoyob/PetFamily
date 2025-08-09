using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Domain.SpeciesAggregate.BreedEntity;

public sealed class Breed : Shared.Entity<BreedId>
{
    public string Name { get; private set; } = null!;

    private Breed(BreedId id) : base(id) { }

    private Breed(BreedId id, string name) : base(id)
    {
        Name = name;
    }

    public static Result<Breed, Error> Create(BreedId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");

        if (name.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Name");

        return new Breed(id, name);
    }
}