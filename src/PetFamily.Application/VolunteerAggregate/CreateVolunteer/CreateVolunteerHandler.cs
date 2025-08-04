using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Application.VolunteerAggregate.CreateVolunteer;

public class CreateVolunteerHandler(
    IVolunteerRepository volunteerRepository,
    IValidator<CreateVolunteerCommand> validator)
{
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var fullname = FullName.Create(
            command.Request.FullName.LastName,
            command.Request.FullName.FirstName,
            command.Request.FullName.Patronymic).Value;
        
        var emailResult = Email.Create(command.Request.Email).Value;
        var volunteerByEmail = await volunteerRepository.GetByEmail(emailResult, cancellationToken);
        if (volunteerByEmail.IsSuccess)
            return Errors.General.AlreadyExists("Email").ToErrorList();
        
        var description  = command.Request.Description;
        
        var yearsOfExperience = command.Request.YearsOfExperience;
        
        var phoneNumberResult = PhoneNumber.Create(command.Request.PhoneNumber).Value;
        var volunteerByPhoneNumber = await volunteerRepository.GetByPhoneNumber(phoneNumberResult, cancellationToken);
        if (volunteerByPhoneNumber.IsSuccess)
            return Errors.General.AlreadyExists("PhoneNumber").ToErrorList();

        var volunteerResult = Volunteer.Create(
            volunteerId,
            fullname,
            emailResult,
            description,
            phoneNumberResult,
            yearsOfExperience
            );
        
        if (volunteerResult.IsFailure) 
            return volunteerResult.Error.ToErrorList();
        
        await volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id.Value;
    }
}