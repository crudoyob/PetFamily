using PetFamily.Contracts.Requests.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.GetVolunteerById;

public record GetVolunteerByIdCommand(GetVolunteerByIdRequest Request);
    
public static class GetByIdCommandExtension
{
    public static GetVolunteerByIdCommand ToCommand(this GetVolunteerByIdRequest request)
    {
        return new GetVolunteerByIdCommand(request);
    }
}