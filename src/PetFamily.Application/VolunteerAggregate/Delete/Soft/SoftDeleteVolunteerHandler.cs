using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerAggregate.Delete.Hard;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Application.VolunteerAggregate.Delete.Soft;

public class SoftDeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

    public SoftDeleteVolunteerHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<SoftDeleteVolunteerCommand> validator,
        ILogger<SoftDeleteVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
     public async Task<Result<Guid, ErrorList>> Handle(
         SoftDeleteVolunteerCommand command, 
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
        
        volunteerResult.Value.Delete();
        
        await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Volunteer with ID {VolunteerId} has been successfully soft deleted", volunteerResult.Value.Id);
        
        return (Guid) volunteerResult.Value.Id;
    }
}