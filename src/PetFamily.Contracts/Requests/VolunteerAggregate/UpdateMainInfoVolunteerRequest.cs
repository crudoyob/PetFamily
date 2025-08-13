using PetFamily.Contracts.Dtos.VolunteerAggregate;

namespace PetFamily.Contracts.Requests.VolunteerAggregate;

public record UpdateMainInfoVolunteerRequest(
    FullNameDto FullName);