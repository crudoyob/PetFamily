using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity;

public sealed class Pet : Shared.Ids.Entity<PetId>
{
    private readonly List<HelpRequisite> _helpRequisites = new();

    public Volunteer Volunteer { get; private set; } = null!;
    public string Nickname { get; private set; } = null!;
    public SpeciesBreed SpeciesBreed { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Color { get; private set; } = null!;
    public string HealthInfo { get; private set; } = null!;
    public Location Location { get; private set; } = null!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public bool IsNeutered { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public HelpStatus HelpStatus { get; private set; } = null!;
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public DateTime CreatedAt { get; private set; }

    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        string nickname,
        SpeciesBreed speciesBreed,
        string description,
        string color,
        string healthInfo,
        Location location,
        double weight, 
        double height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateOnly birthDate, 
        bool isVaccinated, 
        HelpStatus helpStatus
        ) : base(id)
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

    public static Result<Pet, Error> Create(
        PetId id,
        string nickname,
        SpeciesBreed speciesBreed,
        string description,
        string color, 
        string healthInfo, 
        Location location, 
        double weight, 
        double height, 
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateOnly birthDate, 
        bool isVaccinated, 
        HelpStatus helpStatus)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Errors.General.ValueIsRequired("Nickname");

        if (nickname.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Nickname");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");

        if (description.Length > LengthConstants.LENGTH1500)
            return Errors.General.ValueIsInvalid("Description");

        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsRequired("Color");

        if (color.Length > LengthConstants.LENGTH500)
            return Errors.General.ValueIsInvalid("Color");

        if (string.IsNullOrWhiteSpace(healthInfo))
            return Errors.General.ValueIsRequired("HealthInfo");

        if (healthInfo.Length > LengthConstants.LENGTH1500)
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

        return new Pet(id,
            nickname, 
            speciesBreed,
            description,
            color, 
            healthInfo, 
            location, 
            weight, 
            height,
            phoneNumber, 
            isNeutered, 
            birthDate, 
            isVaccinated, 
            helpStatus);
    }
}