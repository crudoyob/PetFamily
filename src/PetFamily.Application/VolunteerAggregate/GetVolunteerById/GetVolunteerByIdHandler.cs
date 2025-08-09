using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.VolunteerAggregate.CreateVolunteer;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.GetVolunteerById;

public class GetVolunteerByIdHandler
{
    
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    
    public GetVolunteerByIdHandler(
        IVolunteerRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _logger = logger;
    }
    
    public async Task<Result<Volunteer, Error>> Handle(
        GetVolunteerByIdCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(command.Request.VolunteerId);
        
        var result = await _volunteerRepository.GetById(volunteerId, cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError("Failed to retrieve Volunteer with ID {VolunteerId}. Reason: {Error}",
                volunteerId, result.Error);
            return result.Error;
        }

        _logger.LogInformation("Successfully retrieved Volunteer with ID {VolunteerId}", volunteerId);
        return result.Value;
    }
}