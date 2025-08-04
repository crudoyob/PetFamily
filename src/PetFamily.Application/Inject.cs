using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.VolunteerAggregate.CreateVolunteer;
using PetFamily.Application.VolunteerAggregate.GetVolunteerById;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<GetVolunteerByIdHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}