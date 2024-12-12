using FacilityManager.Application;

namespace FacilityManager.App.Startup;

public static class ServiceRegistration
{
    // Registers all services from the Application layer as their corresponding interfaces with a scoped lifetime.
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(classes => classes
                .Where(type => type.IsClass && type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}