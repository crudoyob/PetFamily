using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Application.VolunteerAggregate.Delete.Hard;

public class HardDeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<HardDeleteVolunteerCommand> _validator;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public HardDeleteVolunteerHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<HardDeleteVolunteerCommand> validator,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
     public async Task<Result<Guid, ErrorList>> Handle(
         HardDeleteVolunteerCommand command, 
         CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for HardDeleteVolunteerCommand. Errors: {@Errors}",
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
        
        await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Volunteer with ID {VolunteerId} has been successfully hard deleted", volunteerResult.Value.Id);
        
        return (Guid) volunteerResult.Value.Id;
    }
}