﻿namespace PetFamily.Domain.Shared.Ids;

public record BreedId
{
    public Guid Value { get; }
    
    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public static BreedId NewBreedId() => new(Guid.NewGuid());
    public static BreedId Empty() => new(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);
    
    public static implicit operator Guid(BreedId breedId)
    {
        ArgumentNullException.ThrowIfNull(breedId);
        return breedId.Value;
    }
}