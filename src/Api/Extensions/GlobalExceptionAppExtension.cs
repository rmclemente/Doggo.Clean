using Domain.Core.SeedWork;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Extensions;

public static class GlobalExceptionAppExtension
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var response = context.Response;
                response.ContentType = "application/problem+json";
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error;

                switch (exception)
                {
                    case DomainException domainException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        var errors = new Dictionary<string, string[]>
                        {
                            [nameof(DomainException)] = new string[] { domainException.Message }
                        };

                        await response.WriteAsJsonAsync(new ValidationProblemDetails(errors)
                        {
                            Instance = context.Request.HttpContext.Request.Path,
                            Title = "Domain Exception ocurred",
                            Status = StatusCodes.Status400BadRequest,
                            Detail = domainException.Message
                        },
                        new JsonSerializerOptions()
                        {
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        });
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new ProblemDetails
                        {
                            Instance = context.Request.HttpContext.Request.Path,
                            Title = "Unhandled error occurred",
                            Status = StatusCodes.Status500InternalServerError,
                            Detail = "Oh No! We are facing an impossible situation and we were not able to process your request. Please, try again in a few minutes."
                        },
                        new JsonSerializerOptions()
                        {
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        });
                        break;
                }
            });
        });
    }
}
