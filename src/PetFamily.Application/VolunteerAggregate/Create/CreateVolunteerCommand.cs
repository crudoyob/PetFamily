using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Create;

public record CreateVolunteerCommand(CreateVolunteerRequest Request);

public static class CreateVolunteerCommandExtension
{
    public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
    {
        return new CreateVolunteerCommand(request);
    }
}