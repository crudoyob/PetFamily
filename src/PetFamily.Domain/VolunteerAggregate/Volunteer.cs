using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate;

public class Volunteer : EntityId<VolunteerId>
{
    private readonly List<SocialNetwork> _socialNetworks = new();
    private readonly List<HelpRequisite> _helpRequisites = new();
    private readonly List<Pet> _pets = new();
    
    private Volunteer(VolunteerId volunteerId) : base(volunteerId)
    {
    }

    public Volunteer(VolunteerId volunteerId, FullName fullName, Email email, string description, 
        PhoneNumber phoneNumber) : base(volunteerId)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
    }
    
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public string Description { get; private set; }
    public int YearsOfExperience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public IReadOnlyList<Pet> Pets => _pets;
    
    public int CountFoundHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);
    public int CountLookingForHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);
    public int CountNeedsHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedsHelp);
    
    public int NumberOfPetsFoundHome => CountFoundHomePets();
    public int NumberOfPetsLookingForHome => CountLookingForHomePets();
    public int NumberOfPetsNeedsHelp => CountNeedsHelpPets();
    
    public static Result<Volunteer> Create(VolunteerId volunteerId, FullName fullName, Email email, string description, 
        PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Описание волонтёра не может быть пустым");

        return Result.Success(new Volunteer(volunteerId, fullName, email, description, phoneNumber));
    }
    
}