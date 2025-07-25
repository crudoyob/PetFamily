﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesAggregate.BreedEntity;
using PetFamily.Domain.SpeciesAggregate.ValueObjects;

namespace PetFamily.Domain.SpeciesAggregate;

public class Species : EntityId<SpeciesId>
{
    private readonly List<Breed> _breeds = new();

    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    private Species(SpeciesId id) : base(id) { }

    private Species(SpeciesId id, string name) : base(id)
    {
        Name = name;
    }

    public static Result<Species> Create(SpeciesId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Имя вида питомца не может быть пустым");

        if (name.Length > LengthConstants.LENGTH100)
            return Result.Failure<Species>
                ($"Имя вида питомца не может превышать {LengthConstants.LENGTH100} символов");

        return Result.Success(new Species(id, name));
    }
}