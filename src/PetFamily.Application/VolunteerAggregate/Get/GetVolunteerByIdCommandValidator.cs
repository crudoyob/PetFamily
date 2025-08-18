using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.VolunteerAggregate.Get;

public class GetVolunteerByIdCommandValidator : AbstractValidator<GetVolunteerByIdCommand>
{
    public GetVolunteerByIdCommandValidator()
    {
        RuleFor(g => g.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}