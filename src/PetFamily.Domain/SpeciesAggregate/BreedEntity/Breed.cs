using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesAggregate.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.BreedEntity.ValueObjects;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SpeciesAggregate.BreedEntity;

public class Breed : EntityId<BreedId>
{
    private Breed(BreedId breedId) : base(breedId)
    {
    }
    
    public Breed(BreedId breedId, string name) : base(breedId)
    {
        Name = name;
    }

    public string Name { get; private set; }
    
    public static Result<Breed> Create(BreedId breedId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Breed>("Имя породы питомца не может быть пустым");
        
        return Result.Success(new Breed(breedId, name));
    }
}