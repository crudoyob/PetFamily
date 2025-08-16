using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerAggregate.UpdateSocialNetworks;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.UpdateHelpRequisites;

public class UpdateHelpRequisitesHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateHelpRequisitesCommand> _validator;
    private readonly ILogger<UpdateHelpRequisitesHandler> _logger;

    public UpdateHelpRequisitesHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<UpdateHelpRequisitesCommand> validator,
        ILogger<UpdateHelpRequisitesHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateHelpRequisitesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateHelpRequisitesCommand. Errors: {@Errors}",
                validationResult.Errors.Select(e => e.ErrorMessage));
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            _logger.LogWarning("Volunteer not found. VolunteerId: {VolunteerId}", volunteerId);

            return volunteerResult.Error.ToErrorList();
        }
        
        var helpRequisites = new List<HelpRequisite>(command.Request.HelpRequisites.Count());

        helpRequisites.AddRange(from s in command.Request.HelpRequisites
            let helpRequisite = HelpRequisite.Create(s.Name, s.Description)
            select helpRequisite.Value);

        volunteerResult.Value.UpdateHelpRequisites(helpRequisites);

        _logger.LogInformation("The volunteer's help requisites with ID {VolunteerId}" +
                               " have been successfully updated. Help requisites: {@HelpRequisites}",
            volunteerId,
            helpRequisites.Select(s => new { s.Name, s.Description }));
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        return result;
    }
}