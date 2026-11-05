using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sample.Shared.Kernel;

namespace Sample.Host.Middleware;

public sealed class AppExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetails;

    public AppExceptionHandler(IProblemDetailsService problemDetails) =>
        _problemDetails = problemDetails;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct
    )
    {
        if (exception is not AppException appEx)
            return false;

        var status = MapStatus(appEx);

        httpContext.Response.StatusCode = status;

        return await _problemDetails.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Title = appEx.Code,
                    Detail = appEx.Message,
                    Type = $"https://httpstatuses.io/{status}",
                },
            }
        );
    }

    private static int MapStatus(AppException appEx) =>
        appEx switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };
}
