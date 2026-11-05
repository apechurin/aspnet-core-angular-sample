using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Ports;
using Sample.Domain.Tasks;
using Sample.Infrastructure.Adapters;
using Sample.Infrastructure.Persistence;
using Sample.Infrastructure.Persistence.Repositories;

namespace Sample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<TaskDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("TaskDb"),
                options =>
                {
                    options.MigrationsHistoryTable("__EFMigrationsHistory", "sample");
                }
            )
        );

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskQueries, TaskQueries>();
        services.AddScoped<IUnitOfWork, TaskUnitOfWork>();

        return services;
    }
}
