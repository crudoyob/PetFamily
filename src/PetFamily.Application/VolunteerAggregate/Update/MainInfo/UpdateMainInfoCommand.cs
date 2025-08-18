using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Update.MainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, UpdateMainInfoRequest Request);

public static class UpdateMainInfoCommandExtension
{
    public static UpdateMainInfoCommand ToCommand(this UpdateMainInfoRequest request, Guid volunteerId)
    {
        return new UpdateMainInfoCommand(volunteerId, request);
    }
}