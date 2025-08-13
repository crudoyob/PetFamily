using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity;

public sealed class Pet : Shared.Entity<PetId>
{
    private readonly List<HelpRequisite> _helpRequisites = new();
    private readonly List<VaccinationInfo> _vaccinationInfo = new();

    public Volunteer Volunteer { get; private set; } = null!;
    public Nickname Nickname { get; private set; } = null!;
    public SpeciesBreed SpeciesBreed { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Color Color { get; private set; } = null!;
    public HealthInfo HealthInfo { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public IReadOnlyList<VaccinationInfo> VaccinationInfo => _vaccinationInfo;
    public HelpStatus HelpStatus { get; private set; } = null!;
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public DateTime CreatedAt { get; private set; }

    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        Nickname nickname,
        SpeciesBreed speciesBreed,
        Description description,
        Color color,
        HealthInfo healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        DateOnly birthDate,
        HelpStatus helpStatus
        ) : base(id)
    {
        Nickname = nickname;
        SpeciesBreed = speciesBreed;
        Description = description;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        HelpStatus = helpStatus;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<Pet, Error> Create(
        PetId id,
        Nickname nickname,
        SpeciesBreed speciesBreed,
        Description description,
        Color color, 
        HealthInfo healthInfo, 
        Address address, 
        PhoneNumber phoneNumber,
        DateOnly birthDate, 
        HelpStatus helpStatus)
    {
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
            address, 
            phoneNumber, 
            birthDate, 
            helpStatus);
    }
}