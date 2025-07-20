using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var fullnameResult = FullName.Create(command.FullName.LastName, command.FullName.FirstName, command.FullName.Patronymic);
        if(fullnameResult.IsFailure)
            return fullnameResult.Error;
        
        var emailResult = Email.Create(command.Email);
        if(emailResult.IsFailure)
            return  emailResult.Error;
        
        var volunteerByEmail = await _volunteerRepository.GetByEmail(emailResult.Value, cancellationToken);
        if (volunteerByEmail.IsSuccess)
            return Errors.Volunteer.AlreadyExists();
        
        var description  = command.Description;
        
        var yearsOfExperience = command.YearsOfExperience;
        
        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if(phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;
        
        var volunteerByPhoneNumber = await _volunteerRepository.GetByPhoneNumber(phoneNumberResult.Value, cancellationToken);
        if (volunteerByPhoneNumber.IsSuccess)
            return Errors.Volunteer.AlreadyExists();

        var volunteerResult = Volunteer.Create(
            volunteerId,
            fullnameResult.Value,
            emailResult.Value,
            description,
            phoneNumberResult.Value,
            yearsOfExperience
            );
        
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id.Value;
    }
}