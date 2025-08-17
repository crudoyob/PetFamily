using PetFamily.Contracts.Dtos;
using PetFamily.Contracts.Dtos.VolunteerAggregate;

namespace PetFamily.Contracts.Requests.VolunteerAggregate;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    YearsOfExperienceDto YearsOfExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<HelpRequisiteDto> HelpRequisites);
    
    