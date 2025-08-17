using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Delete.Hard;

public record HardDeleteVolunteerCommand(HardDeleteVolunteerRequest Request);
    
public static class HardDeleteVolunteerCommandExtension
{
    public static HardDeleteVolunteerCommand ToCommand(this HardDeleteVolunteerRequest request)
    {
        return new HardDeleteVolunteerCommand(request);
    }
}