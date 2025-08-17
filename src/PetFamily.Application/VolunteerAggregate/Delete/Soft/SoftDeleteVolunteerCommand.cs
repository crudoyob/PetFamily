using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Delete.Soft;

public record SoftDeleteVolunteerCommand(SoftDeleteVolunteerRequest Request);
    
public static class SoftDeleteVolunteerCommandExtension
{
    public static SoftDeleteVolunteerCommand ToCommand(this SoftDeleteVolunteerRequest request)
    {
        return new SoftDeleteVolunteerCommand(request);
    }
}