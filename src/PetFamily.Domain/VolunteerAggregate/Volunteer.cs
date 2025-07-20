using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate;

public sealed class Volunteer : EntityId<VolunteerId>
{
    private readonly List<SocialNetwork> _socialNetworks = new();
    private readonly List<HelpRequisite> _helpRequisites = new();
    private readonly List<Pet> _pets = new();

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public string Description { get; private set; }
    public int YearsOfExperience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public IReadOnlyList<Pet> Pets => _pets;

    private Volunteer(VolunteerId id) : base(id) { }

    private Volunteer(VolunteerId id, FullName fullName, Email email, string description,
        PhoneNumber phoneNumber, int yearsOfExperience = 0) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }

    public int CountFoundHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);
    public int CountLookingForHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);
    public int CountNeedsHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedsHelp);

    public int NumberOfPetsFoundHome => CountFoundHomePets();
    public int NumberOfPetsLookingForHome => CountLookingForHomePets();
    public int NumberOfPetsNeedsHelp => CountNeedsHelpPets();

    public static Result<Volunteer, Error> Create(VolunteerId id, FullName fullName, Email email, string description,
        PhoneNumber phoneNumber, int yearsOfExperience = 0)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");

        if (description.Length > LengthConstants.LENGTH1500)
            return Errors.General.ValueIsInvalid("Description");

        if (yearsOfExperience < 0)
            return Errors.General.ValueIsInvalid("YearsOfExperience");

        return new Volunteer(id, fullName, email, description, phoneNumber, yearsOfExperience);
    }
}