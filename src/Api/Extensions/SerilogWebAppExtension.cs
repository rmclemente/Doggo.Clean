using Serilog;

namespace Api.Extensions;

public static class SerilogWebAppExtension
{
    public static WebApplicationBuilder AddSerilogConfiguration(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
            .ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }

    public static WebApplication UseSerilogConfiguration(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}
