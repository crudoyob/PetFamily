using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public enum Sex
{
    Unknown,
    Male,
    Female,
    Other
}

public sealed record HealthInfo
{
    public string Description { get; }
    public double Weight { get; }
    public double Height { get; }
    public Sex Sex { get; }
    public bool IsNeutered { get; }

    private HealthInfo(
        string description,
        double weight,
        double height,
        Sex sex,
        bool isNeutered)
    {
        Description = description;
        Weight = weight;
        Height = height;
        Sex = sex;
        IsNeutered = isNeutered;
    }

    public static Result<HealthInfo, Error> Create(
        string description,
        double weight,
        double height,
        Sex sex,
        bool isNeutered)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("Description");

        if (description.Length > LengthConstants.LENGTH1500)
            return Errors.General.ValueIsInvalid("Description");

        if (weight < LengthConstants.LENGTH0 || weight > LengthConstants.LENGTH150)
            return Errors.General.ValueIsInvalid("Weight");

        if (height < LengthConstants.LENGTH0 || height > LengthConstants.LENGTH150)
            return Errors.General.ValueIsInvalid("Height");

        if (!Enum.IsDefined(typeof(Sex), sex))
            return Errors.General.ValueIsInvalid("Sex");

        return new HealthInfo(description, weight, height, sex, isNeutered);
    }

    public override string ToString()
    {
        return $"{Description}, Weight: {Weight} kg, Height: {Height} cm, Gender: {Sex}, Neutered: {IsNeutered}";
    }
}