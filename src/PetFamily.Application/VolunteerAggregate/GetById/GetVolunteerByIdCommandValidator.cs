using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.VolunteerAggregate.GetVolunteerById;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerAggregate.GetById;

public class GetVolunteerByIdCommandValidator : AbstractValidator<GetVolunteerByIdCommand>
{
    public GetVolunteerByIdCommandValidator()
    {
        RuleFor(g => g.Request.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}