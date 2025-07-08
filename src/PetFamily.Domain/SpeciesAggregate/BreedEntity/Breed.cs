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

    public static Result<Breed> Create(BreedId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Breed>("Имя породы питомца не может быть пустым");

        if (name.Length > LengthConstants.Length100)
            return Result.Failure<Breed>
                ($"Имя породы питомца не может превышать {LengthConstants.Length100} символов");

        return Result.Success(new Breed(id, name));
    }
}