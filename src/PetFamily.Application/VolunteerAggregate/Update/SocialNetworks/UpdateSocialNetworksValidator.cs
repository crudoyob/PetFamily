using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate.Update.SocialNetworks;

public class UpdateSocialNetworksValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksValidator()
    {
        RuleForEach(u => u.Request.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.Url));
    }
}