using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.VolunteerAggregate.Restore;

public class RestoreVolunteerCommandValidator : AbstractValidator<RestoreVolunteerCommand>
{
    public RestoreVolunteerCommandValidator()
    {
        RuleFor(r => r.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}