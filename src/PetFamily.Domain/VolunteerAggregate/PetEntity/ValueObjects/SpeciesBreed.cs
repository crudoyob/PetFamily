using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesAggregate.BreedEntity.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public record SpeciesBreed
{
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }
    
    private SpeciesBreed(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<SpeciesBreed> Create(SpeciesId speciesId, BreedId breedId)
    {
        return new SpeciesBreed(speciesId, breedId);
    }
}