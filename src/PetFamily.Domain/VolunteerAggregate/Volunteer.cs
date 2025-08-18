using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate;

public sealed class Volunteer : BaseEntity<VolunteerId>, ISoftDeletable
{
    private readonly List<SocialNetwork> _socialNetworks = new();
    private readonly List<HelpRequisite> _helpRequisites = new();
    private readonly List<Pet> _pets = new();

    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public YearsOfExperience YearsOfExperience { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public bool IsDeleted { get; private set;  }
    public DateTime? DeletionDate { get; private set; } = null;
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<HelpRequisite> HelpRequisites => _helpRequisites;
    public IReadOnlyList<Pet> Pets => _pets;

    private Volunteer(VolunteerId id) : base(id) { }

    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phoneNumber,
        YearsOfExperience yearsOfExperience
        ) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }

    public int CountFoundHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);
    public int CountLookingForHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);
    public int CountNeedsHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedsHelp);

    public int NumberOfPetsFoundHome => CountFoundHomePets();
    public int NumberOfPetsLookingForHome => CountLookingForHomePets();
    public int NumberOfPetsNeedsHelp => CountNeedsHelpPets();

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phoneNumber,
        YearsOfExperience yearsOfExperience)
    {
        return new Volunteer(
            id,
            fullName,
            email,
            description,
            phoneNumber,
            yearsOfExperience);
    }

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        YearsOfExperience yearsOfExperience,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }
    
    public void UpdateSocialNetworks(
        IEnumerable<SocialNetwork> socialNetworks)
    {
        _socialNetworks.Clear();
        _socialNetworks.AddRange(socialNetworks);
    }
    
    public void UpdateHelpRequisites(
        IEnumerable<HelpRequisite> helpRequisites)
    {
        _helpRequisites.Clear();
        _helpRequisites.AddRange(helpRequisites);
    }
    
    public  void Delete(bool cascade = true)
    {
        if (IsDeleted)
            return;
        
        IsDeleted = true;
        DeletionDate = DateTime.UtcNow;
        
        if (cascade)
            _pets.ForEach(pet => pet.Delete(cascade));
    }

    public void Restore(bool cascade = true)
    {
        if (IsDeleted == false)
            return;
        
        IsDeleted = false;
        DeletionDate = null;
        
        if (cascade)
            _pets.ForEach(pet => pet.Restore(cascade));
    }
}