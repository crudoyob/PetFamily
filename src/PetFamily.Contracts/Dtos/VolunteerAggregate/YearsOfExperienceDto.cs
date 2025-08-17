namespace PetFamily.Contracts.Dtos.VolunteerAggregate;

public record YearsOfExperienceDto(
    int Years = 0,
    bool IsVerified = false);