using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;

    public UpdateSocialNetworksHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<UpdateSocialNetworksCommand> validator,
        ILogger<UpdateSocialNetworksHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateSocialNetworksCommand. Errors: {@Errors}",
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
        
        var socialNetworks = new List<SocialNetwork>(command.Request.SocialNetworks.Count());

        socialNetworks.AddRange(from s in command.Request.SocialNetworks
            let socialResult = SocialNetwork.Create(s.Name, s.Url)
            select socialResult.Value);

        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        _logger.LogInformation("The volunteer's social networks with ID {VolunteerId}" +
                               " have been successfully updated. Social networks: {@SocialNetworks}",
            volunteerId,
            socialNetworks.Select(s => new { s.Name, s.Url }));
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        return result;
    }
}