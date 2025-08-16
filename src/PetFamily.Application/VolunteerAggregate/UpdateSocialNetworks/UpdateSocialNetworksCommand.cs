using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, UpdateSocialNetworksRequest Request);

public static class UpdateSocialNetworksCommandExtension
{
    public static UpdateSocialNetworksCommand ToCommand(this UpdateSocialNetworksRequest request, Guid volunteerId)
    {
        return new UpdateSocialNetworksCommand(volunteerId, request);
    }
}