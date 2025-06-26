using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity;

public class Pet : EntityId<PetId>
{
    private readonly List<HelpRequisite> _helpRequisites = new();
    
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
    
    private Pet (PetId petId) : base(petId)
    {
    }

    public Pet (PetId petId, string nickname, SpeciesBreed speciesBreed, string description, string color,
        string healthInfo, Location location, double weight, double height, PhoneNumber phoneNumber, bool isNeutered,
        DateOnly birthDate, bool isVaccinated, HelpStatus helpStatus) : base(petId)
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
    
    public static Result<Pet> Create(PetId petId, string nickname, SpeciesBreed speciesBreed, string description, 
        string color, string healthInfo, Location location, double weight, double height, PhoneNumber phoneNumber,
        bool isNeutered, DateOnly birthDate, bool isVaccinated, HelpStatus helpStatus)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Result.Failure<Pet>("Кличка питомца не может быть пустой");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Pet>("Описание питомца не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<Pet>("Окрас животного не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(healthInfo))
            return Result.Failure<Pet>("Информация о здоровье питомца не может быть пустой");
        
        if(weight < 0 || weight > 150)
            return Result.Failure<Pet>("Некорректный вес питомца: ");
        
        if(height < 0 || height > 150)
            return Result.Failure<Pet>("Некорректный рост питомца");
        
        if (birthDate < DateOnly.FromDateTime(DateTime.Today.AddYears(-35)))
            return Result.Failure<Pet>("Дата рождения питомца не может быть более 35 лет назад");
        
        if (birthDate > DateOnly.FromDateTime(DateTime.Today))
            return Result.Failure<Pet>("Дата рождения питомца не может в будущем");

        var result = new Pet(petId, nickname, speciesBreed, description, color, healthInfo, location, weight, height, 
            phoneNumber, isNeutered, birthDate, isVaccinated, helpStatus);
        
        return Result.Success(result);
    }
}