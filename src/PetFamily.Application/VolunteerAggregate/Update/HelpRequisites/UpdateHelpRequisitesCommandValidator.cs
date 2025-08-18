using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.Update.HelpRequisites;

public class UpdateHelpRequisitesCommandValidator : AbstractValidator<UpdateHelpRequisitesCommand>
{
    public UpdateHelpRequisitesCommandValidator()
    {
        RuleForEach(u => u.Request.HelpRequisites)
            .MustBeValueObject(hr => HelpRequisite.Create(hr.Name, hr.Description));
    }
}