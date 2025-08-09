using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    
    public CreateVolunteerHandler(
        IVolunteerRepository volunteersRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for CreateVolunteerCommand. Errors: {@Errors}",
                validationResult.Errors.Select(e => e.ErrorMessage));
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var fullname = FullName.Create(
            command.Request.FullName.LastName,
            command.Request.FullName.FirstName,
            command.Request.FullName.Patronymic).Value;
        
        var email = Email.Create(command.Request.Email).Value;
        var volunteerByEmail = await _volunteerRepository.GetByEmail(email, cancellationToken);
        if (volunteerByEmail.IsSuccess)
        {
            _logger.LogWarning("Attempt to create Volunteer failed: email {Email} already exists", email.Value);
            return Errors.General.AlreadyExists("Email").ToErrorList();
        }
        
        var description  = command.Request.Description;
        
        var yearsOfExperience = command.Request.YearsOfExperience;
        
        var phoneNumber = PhoneNumber.Create(command.Request.PhoneNumber).Value;
        var volunteerByPhoneNumber = await _volunteerRepository.GetByPhoneNumber(phoneNumber, cancellationToken);
        if (volunteerByPhoneNumber.IsSuccess)
        {
            _logger.LogWarning("Attempt to create Volunteer failed: phone number {PhoneNumber} already exists", phoneNumber.Value);
            return Errors.General.AlreadyExists("PhoneNumber").ToErrorList();
        }

        var volunteerResult = Volunteer.Create(
            volunteerId,
            fullname,
            email,
            description,
            phoneNumber,
            yearsOfExperience
            );

        if (volunteerResult.IsFailure)
        {
            _logger.LogError("Failed to create Volunteer with ID {VolunteerId}. Reason: {Error}", volunteerId, volunteerResult.Error.Message);
            return volunteerResult.Error.ToErrorList();
        }
        
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
        _logger.LogInformation("Volunteer with ID {VolunteerId} was successfully created", volunteerId);

        return volunteerResult.Value.Id.Value;
    }
}