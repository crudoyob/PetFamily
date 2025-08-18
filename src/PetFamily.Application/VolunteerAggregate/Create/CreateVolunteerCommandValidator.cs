using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.Request.FullName).MustBeValueObject(fn => FullName.Create(
            fn.LastName,
            fn.FirstName,
            fn.Patronymic));
        
        RuleFor(c => c.Request.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Request.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.Request.YearsOfExperience).MustBeValueObject(
            ye => YearsOfExperience.Create(
                ye.Years,
                ye.IsVerified));
        
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