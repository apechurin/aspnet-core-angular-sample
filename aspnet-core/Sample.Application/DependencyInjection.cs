using Microsoft.Extensions.DependencyInjection;

namespace Sample.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.Lifetime = ServiceLifetime.Scoped;
        });

        return services;
    }
}
