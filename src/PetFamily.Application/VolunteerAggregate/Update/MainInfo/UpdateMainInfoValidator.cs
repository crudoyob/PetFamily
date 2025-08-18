using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.Update.MainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("VolunteerId"));
        
        RuleFor(u => u.Request.FullName).MustBeValueObject(fn => FullName.Create(
            fn.LastName,
            fn.FirstName,
            fn.Patronymic));
        
        RuleFor(u => u.Request.Email).MustBeValueObject(Email.Create);
        
        RuleFor(u => u.Request.Description).MustBeValueObject(Description.Create);
        
        RuleFor(u => u.Request.YearsOfExperience).MustBeValueObject(
            ye => YearsOfExperience.Create(
                ye.Years,
                ye.IsVerified));
        
        RuleFor(u => u.Request.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}