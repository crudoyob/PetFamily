using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public sealed record SpeciesBreed
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