using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;

namespace Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddProblemDetails(setup =>
        {
            setup.IncludeExceptionDetails = (ctx, env) => false;
            setup.Map<MediaTypeCustomException>(exception => new ProblemDetails
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status400BadRequest,
                Type = exception.Type
            });
            setup.Map<OrderByCustomException>(exception => new ProblemDetails
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status400BadRequest,
                Type = exception.Type
            });
            setup.Map<DataShapingCustomException>(exception => new ProblemDetails
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status400BadRequest,
                Type = exception.Type
            });
            setup.Map<ResourceNotFoundCustomException>(exception => new ProblemDetails
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status400BadRequest,
                Type = exception.Type
            });
        });
        return services;
    }
}
