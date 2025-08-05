using PetFamily.Contracts.Dtos;
using PetFamily.Contracts.Dtos.VolunterAggregate;

namespace PetFamily.Contracts.Requests.VolunteerAggregate;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsOfExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<HelpRequisiteDto> HelpRequisites);
    
    