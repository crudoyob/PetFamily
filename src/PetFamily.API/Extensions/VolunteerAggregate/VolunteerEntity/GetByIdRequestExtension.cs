using PetFamily.Application.Volunteers.GetVolunteerById;
using PetFamily.Contracts.Requests;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Api.Extensions.VolunteerAggregate.Volunteers;

public static class GetByIdRequestExtension
{
    public static GetVolunteerByIdCommand ToCommand(this GetVolunteerByIdRequest request)
    {
        return new GetVolunteerByIdCommand(
            VolunteerId.Create(request.VolunteerId));
    }
}