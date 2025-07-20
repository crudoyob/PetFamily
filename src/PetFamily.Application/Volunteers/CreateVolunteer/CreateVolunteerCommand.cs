using PetFamily.Contracts.Dtos;
using PetFamily.Contracts.Dtos.VolunterAggregate.VolunteerEntity;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    int YearsOfExperience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworks,
    IEnumerable<HelpRequisiteDto> HelpRequisites);