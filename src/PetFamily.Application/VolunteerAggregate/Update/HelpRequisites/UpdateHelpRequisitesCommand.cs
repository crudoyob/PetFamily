using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Update.HelpRequisites;

public record UpdateHelpRequisitesCommand(Guid VolunteerId, UpdateHelpRequisitesRequest Request);

public static class UpdateHelpRequisitesCommandExtension
{
    public static UpdateHelpRequisitesCommand ToCommand(this UpdateHelpRequisitesRequest request, Guid volunteerId)
    {
        return new UpdateHelpRequisitesCommand(volunteerId, request);
    }
}