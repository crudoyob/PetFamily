using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application;
using PetFamily.Application.VolunteerAggregate;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        
        return services;
    }
}