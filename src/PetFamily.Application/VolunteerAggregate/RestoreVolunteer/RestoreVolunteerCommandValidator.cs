using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.VolunteerAggregate.HardDeleteVolunteer;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerAggregate.RestoreVolunteer;

public class RestoreVolunteerCommandValidator : AbstractValidator<RestoreVolunteerCommand>
{
    public RestoreVolunteerCommandValidator()
    {
        RuleFor(g => g.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}