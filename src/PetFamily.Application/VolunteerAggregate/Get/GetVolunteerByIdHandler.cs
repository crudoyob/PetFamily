using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerAggregate.Create;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.Get;

public class GetVolunteerByIdHandler
{
    
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<GetVolunteerByIdCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    
    public GetVolunteerByIdHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<GetVolunteerByIdCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Volunteer, ErrorList>> Handle(
        GetVolunteerByIdCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for GetVolunteerByIdCommand. Errors: {@Errors}",
                validationResult.Errors.Select(e => e.ErrorMessage));
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.Request.VolunteerId);
        
        var result = await _volunteerRepository.GetById(volunteerId, cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError("Failed to retrieve Volunteer with ID {VolunteerId}. Reason: {Error}",
                volunteerId, result.Error);
            return result.Error.ToErrorList();
        }

        _logger.LogInformation("Successfully retrieved Volunteer with ID {VolunteerId}", volunteerId);
        return result.Value;
    }
}