using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.VolunteerAggregate.Delete.Soft;

public class SoftDeleteVolunteerValidator : AbstractValidator<SoftDeleteVolunteerCommand>
{
    public SoftDeleteVolunteerValidator()
    {
        RuleFor(s => s.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}