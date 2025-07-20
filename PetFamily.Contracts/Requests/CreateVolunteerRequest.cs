using PetFamily.Contracts.Dtos;
using PetFamily.Contracts.Dtos.VolunterAggregate.VolunteerEntity;

namespace PetFamily.Contracts.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsOfExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<HelpRequisiteDto> HelpRequisites);