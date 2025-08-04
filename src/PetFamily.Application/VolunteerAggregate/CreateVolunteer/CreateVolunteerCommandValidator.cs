using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.Request.FullName).MustBeValueObject(fn => FullName.Create(
            fn.LastName,
            fn.FirstName,
            fn.Patronymic));
        
        RuleFor(c => c.Request.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Request.Description)
            .MaximumLength(LengthConstants.LENGTH1500)
            .WithError(Errors.General.ValueIsInvalid("description"));
        
        RuleFor(c => c.Request.YearsOfExperience)
            .GreaterThanOrEqualTo(LengthConstants.LENGTH0)
            .WithError(Errors.General.ValueIsInvalid("experienceYears"))
            .LessThanOrEqualTo(LengthConstants.LENGTH100)
            .WithError(Errors.General.ValueIsInvalid("experienceYears"));
        
        RuleFor(c => c.Request.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleForEach(c => c.Request.SocialNetworks).MustBeValueObject(sn =>
            SocialNetwork.Create(
                sn.Name, 
                sn.Url));
        
        RuleForEach(c => c.Request.HelpRequisites).MustBeValueObject(hr =>
            HelpRequisite.Create(
                hr.Name, 
                hr.Description));
    }
}