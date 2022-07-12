using System.Text.Json.Serialization;

namespace Api.Extensions;

public static class ApiWebAppExtension
{
    public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllAllowed",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Location"));
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHealthChecks();
        return builder;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //Always on Top
        app.UseCors("AllAllowed");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseGlobalExceptionHandler();
        }

        if (app.Environment.IsEnvironment("Qa"))
        {
            app.UseDeveloperExceptionPage();
            app.UseGlobalExceptionHandler();
        }

        if (app.Environment.IsStaging())
        {
            app.UseGlobalExceptionHandler();
        }

        if (app.Environment.IsProduction())
        {
            app.UseGlobalExceptionHandler();
        }

        app.UseHttpsRedirection();
        //app.UseRouting();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");

        return app;
    }
}
