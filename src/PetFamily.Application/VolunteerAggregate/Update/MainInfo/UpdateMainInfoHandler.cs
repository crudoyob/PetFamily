using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.Update.MainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateMainInfoCommand. Errors: {@Errors}",
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

        var fullname = FullName.Create(
            command.Request.FullName.LastName,
            command.Request.FullName.FirstName,
            command.Request.FullName.Patronymic).Value;
        
        var email = Email.Create(command.Request.Email).Value;
        
        var description = Description.Create(command.Request.Description).Value;
        
        var yearsOfExperience = YearsOfExperience.Create(
            command.Request.YearsOfExperience.Years,
            command.Request.YearsOfExperience.IsVerified).Value;
        
        var phoneNumber = PhoneNumber.Create(command.Request.PhoneNumber).Value;
        
        volunteerResult.Value.UpdateMainInfo(fullname, email, description, yearsOfExperience, phoneNumber);
        
        _logger.LogInformation(
            "The main information about the volunteer with the ID {VolunteerId} has been updated. " +
            "Full name: {FullName}, " +
            "Email address: {Email}, " +
            "Description: {Description}, " +
            "Years of experience: {YearsOfExperience} (Verified: {IsVerified}), " +
            "Phone number: {PhoneNumber}",
            volunteerId,
            $"{fullname.LastName} {fullname.FirstName} {(fullname.Patronymic ?? "-")}",
            email.Value,
            description.Value,
            yearsOfExperience.Years,
            yearsOfExperience.IsVerified,
            phoneNumber.Value);
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        return result;
    }
}