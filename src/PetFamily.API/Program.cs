using PetFamily.Api;
using PetFamily.Api.Extensions;
using PetFamily.Api.Middlewares;
using PetFamily.Infrastructure;
using PetFamily.Application;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                 ?? throw new ArgumentNullException("Seq"))
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName() 
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.MVC", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();