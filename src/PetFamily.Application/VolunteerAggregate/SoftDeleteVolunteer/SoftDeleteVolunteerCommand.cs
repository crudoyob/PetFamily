using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.SoftDeleteVolunteer;

public record SoftDeleteVolunteerCommand(SoftDeleteVolunteerRequest Request);
    
public static class HardDeleteVolunteerCommandExtension
{
    public static SoftDeleteVolunteerCommand ToCommand(this SoftDeleteVolunteerRequest request)
    {
        return new SoftDeleteVolunteerCommand(request);
    }
}