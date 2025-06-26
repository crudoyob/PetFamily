using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesAggregate.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.BreedEntity;

namespace PetFamily.Domain.SpeciesAggregate;

public class Species : EntityId<SpeciesId>
{
    private readonly List<Breed> _breeds;
    
    private Species(SpeciesId speciesId) : base(speciesId)
    {
    }

    public Species(SpeciesId speciesId, string name) : base(speciesId)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species> Create(SpeciesId speciesId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Имя вида питомца не может быть пустым");
        
        return Result.Success(new Species(speciesId, name));
    }
}