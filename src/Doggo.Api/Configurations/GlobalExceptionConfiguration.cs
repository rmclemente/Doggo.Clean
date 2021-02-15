using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Doggo.Api.Configurations
{
    public static class GlobalExceptionConfiguration
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var problemDetails = new ProblemDetails
                    {
                        Type = "about:blank",
                        Instance = context.Request.HttpContext.Request.Path,
                        Title = exceptionHandlerFeature.Error.TargetSite.Name,
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = exceptionHandlerFeature.Error.Message
                    };

                    //var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                    //logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                    await context
                          .Response
                          .WriteAsync(JsonConvert.SerializeObject(problemDetails,
                                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                });
            });
        }
    }
}
