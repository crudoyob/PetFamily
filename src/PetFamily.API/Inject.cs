using Serilog;

namespace PetFamily.Api;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSerilog();
        
        return services;
    }
}