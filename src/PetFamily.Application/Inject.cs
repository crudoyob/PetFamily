using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.VolunteerAggregate.CreateVolunteer;
using PetFamily.Application.VolunteerAggregate.GetVolunteerById;
using PetFamily.Application.VolunteerAggregate.UpdateHelpRequisites;
using PetFamily.Application.VolunteerAggregate.UpdateMainInfo;
using PetFamily.Application.VolunteerAggregate.UpdateSocialNetworks;

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

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}