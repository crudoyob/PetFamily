using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Application.VolunteerAggregate.Restore;

public class RestoreVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<RestoreVolunteerCommand> _validator;
    private readonly ILogger<RestoreVolunteerHandler> _logger;

    public RestoreVolunteerHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<RestoreVolunteerCommand> validator,
        ILogger<RestoreVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
     public async Task<Result<Guid, ErrorList>> Handle(
         RestoreVolunteerCommand command, 
         CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for RestoreVolunteerCommand. Errors: {@Errors}",
                validationResult.Errors.Select(e => e.ErrorMessage));
            return validationResult.ToErrorList();
        }

        var volunteerId = VolunteerId.Create(command.Request.VolunteerId);

        var volunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            _logger.LogWarning("Volunteer not found. VolunteerId: {VolunteerId}", volunteerId);

            return volunteerResult.Error.ToErrorList();
        }
        
        volunteerResult.Value.Restore();
        
        await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Volunteer with ID {VolunteerId} has been successfully restored", volunteerResult.Value.Id);
        
        return (Guid) volunteerResult.Value.Id;
    }
}