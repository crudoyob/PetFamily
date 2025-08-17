using PetFamily.Contracts.Dtos.VolunteerAggregate;

namespace PetFamily.Contracts.Requests.VolunteerAggregate;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    YearsOfExperienceDto YearsOfExperience,
    string PhoneNumber);