using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity;

public class Pet : EntityId<PetId>
{
    private readonly List<HelpRequisite> _helpRequisites = new();

    public Volunteer Volunteer { get; private set; }
    public string Nickname { get; private set; }
    public SpeciesBreed SpeciesBreed { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string HealthInfo { get; private set; }
    public Location Location { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public bool IsNeutered { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public HelpStatus HelpStatus { get; private set; }
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public DateTime CreatedAt { get; private set; }

    private Pet(PetId id) : base(id) { }

    private Pet(PetId id, string nickname, SpeciesBreed speciesBreed, string description, string color,
        string healthInfo, Location location, double weight, double height, PhoneNumber phoneNumber, bool isNeutered,
        DateOnly birthDate, bool isVaccinated, HelpStatus helpStatus) : base(id)
    {
        Nickname = nickname;
        SpeciesBreed = speciesBreed;
        Description = description;
        Color = color;
        HealthInfo = healthInfo;
        Location = location;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<Pet, Error> Create(PetId id, string nickname, SpeciesBreed speciesBreed, string description,
        string color, string healthInfo, Location location, double weight, double height, PhoneNumber phoneNumber,
        bool isNeutered, DateOnly birthDate, bool isVaccinated, HelpStatus helpStatus)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Errors.General.ValueIsRequired("Nickname");

        if (nickname.Length > LengthConstants.Length100)
            return Errors.General.ValueIsInvalid("Nickname");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");

        if (description.Length > LengthConstants.Length1500)
            return Errors.General.ValueIsInvalid("Description");

        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsRequired("Color");

        if (color.Length > LengthConstants.Length500)
            return Errors.General.ValueIsInvalid("Color");

        if (string.IsNullOrWhiteSpace(healthInfo))
            return Errors.General.ValueIsRequired("HealthInfo");

        if (healthInfo.Length > LengthConstants.Length1500)
            return Errors.General.ValueIsInvalid("HealthInfo");

        if (weight < 0)
            return Errors.General.ValueIsInvalid("Weight");

        if (weight > 150)
            return Errors.General.ValueIsInvalid("Weight");

        if (height < 0)
            return Errors.General.ValueIsInvalid("Height");

        if (height > 150)
            return Errors.General.ValueIsInvalid("Height");

        if (birthDate < DateOnly.FromDateTime(DateTime.Today.AddYears(-35)))
            return Errors.General.ValueIsInvalid("BirthDate");

        if (birthDate > DateOnly.FromDateTime(DateTime.Today))
            return Errors.General.ValueIsInvalid("BirthDate");

        return new Pet(id, nickname, speciesBreed, description, color, healthInfo, location, weight, height,
            phoneNumber, isNeutered, birthDate, isVaccinated, helpStatus);
    }
}