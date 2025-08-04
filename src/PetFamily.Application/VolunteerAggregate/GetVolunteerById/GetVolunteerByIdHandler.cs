using CSharpFunctionalExtensions;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.GetVolunteerById;

public class GetVolunteerByIdHandler(IVolunteerRepository volunteerRepository)
{
    public async Task<Result<Volunteer, Error>> Handle(GetVolunteerByIdCommand command, CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(command.Request.VolunteerId);
        
        var result = await volunteerRepository.GetById(volunteerId, cancellationToken);

        if(result.IsFailure)
            return result.Error;

        return result.Value;
    }
}