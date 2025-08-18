using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Restore;

public record RestoreVolunteerCommand(RestoreVolunteerRequest Request);
    
public static class RestoreVolunteerCommandExtension
{
    public static RestoreVolunteerCommand ToCommand(this RestoreVolunteerRequest request)
    {
        return new RestoreVolunteerCommand(request);
    }
}