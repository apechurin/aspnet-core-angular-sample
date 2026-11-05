using Sample.Api;
using Sample.Application;
using Sample.Host.Middleware;
using Sample.Infrastructure;

namespace Host;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<AppExceptionHandler>();

        builder.Services.AddTaskApplication().AddTaskInfrastructure(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapTaskApi();

        app.Run();
    }
}
