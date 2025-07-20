using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.Volunteers.GetVolunteerById;

public class GetVolunteerByIdHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public GetVolunteerByIdHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Volunteer, Error>> Handle(GetVolunteerByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);

        if(result.IsFailure)
            return result.Error;

        return result.Value;
    }
}