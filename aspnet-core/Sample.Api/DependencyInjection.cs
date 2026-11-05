using Microsoft.AspNetCore.Routing;
using Sample.Api.Endpoints;

namespace Sample.Api;

public static class DependencyInjection
{
    public static IEndpointRouteBuilder MapTaskApi(this IEndpointRouteBuilder app)
    {
        app.MapTaskEndpoints();

        return app;
    }
}
