using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Contracts.Requests;

namespace PetFamily.Api.Extensions.VolunteerAggregate.Volunteers;

public static class CreateVolunteerRequestExtensions
{
    public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
    {
        return new CreateVolunteerCommand(
            request.FullName,
            request.Email,
            request.Description,
            request.YearsOfExperience,
            request.PhoneNumber,
            request.SocialNetworks,
            request.HelpRequisites);
    }
}