using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesAggregate.BreedEntity.ValueObjects;

namespace PetFamily.Domain.SpeciesAggregate.BreedEntity;

public class Breed : EntityId<BreedId>
{
    public string Name { get; private set; }

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