using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

public sealed record VaccinationInfo
{
    public string VaccineName { get; }
    public DateTime VaccinationDate { get; }

    private VaccinationInfo(string vaccineName, DateTime vaccinationDate)
    {
        VaccineName = vaccineName;
        VaccinationDate = vaccinationDate;
    }

    public static Result<VaccinationInfo, Error> Create(string vaccineName, DateTime vaccinationDate)
    {
        if (string.IsNullOrWhiteSpace(vaccineName))
            return Errors.General.ValueIsRequired("VaccineName");

        if (vaccineName.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("VaccineName");

        var today = DateTime.UtcNow.Date;

        if (vaccinationDate.Date > today)
            return Errors.General.ValueIsInvalid("VaccinationDate");

        return new VaccinationInfo(vaccineName, vaccinationDate);
    }
}