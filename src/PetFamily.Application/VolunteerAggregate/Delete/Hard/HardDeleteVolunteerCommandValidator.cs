using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.VolunteerAggregate.Delete.Hard;

public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
{
    public HardDeleteVolunteerCommandValidator()
    {
        RuleFor(h => h.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}