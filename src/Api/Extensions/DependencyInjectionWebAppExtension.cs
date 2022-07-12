using Application.Extensions;
using Infrastructure.Data.Extensions;

namespace Api.Extensions;

public static class DependencyInjectionWebAppExtension
{
    public static WebApplicationBuilder AddRegisteredServices(this WebApplicationBuilder builder)
    {
        // ASP.NET HttpContext dependency
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddRepositoryServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        return builder;
    }
}
