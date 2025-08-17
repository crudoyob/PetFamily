using PetFamily.Contracts.Dtos;

namespace PetFamily.Contracts.Requests.VolunteerAggregate;

public record UpdateHelpRequisitesRequest(
    IEnumerable<HelpRequisiteDto> HelpRequisites);