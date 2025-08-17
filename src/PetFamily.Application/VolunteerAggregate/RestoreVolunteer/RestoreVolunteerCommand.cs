using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.RestoreVolunteer;

public record RestoreVolunteerCommand(RestoreVolunteerRequest Request);
    
public static class HardDeleteVolunteerCommandExtension
{
    public static RestoreVolunteerCommand ToCommand(this RestoreVolunteerRequest request)
    {
        return new RestoreVolunteerCommand(request);
    }
}