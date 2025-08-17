using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.VolunteerAggregate.Create;
using PetFamily.Application.VolunteerAggregate.Delete.Hard;
using PetFamily.Application.VolunteerAggregate.Delete.Soft;
using PetFamily.Application.VolunteerAggregate.Get;
using PetFamily.Application.VolunteerAggregate.Restore;
using PetFamily.Application.VolunteerAggregate.Update.HelpRequisites;
using PetFamily.Application.VolunteerAggregate.Update.MainInfo;
using PetFamily.Application.VolunteerAggregate.Update.SocialNetworks;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<GetVolunteerByIdHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSocialNetworksHandler>();
        services.AddScoped<UpdateHelpRequisitesHandler>();
        services.AddScoped<HardDeleteVolunteerHandler>();
        services.AddScoped<SoftDeleteVolunteerHandler>();
        services.AddScoped<RestoreVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}