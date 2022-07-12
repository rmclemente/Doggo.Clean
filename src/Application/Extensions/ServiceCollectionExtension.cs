using Application.SeedWork.Behaviors;
using Domain.Core.SeedWork;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<NotificationContext>();
        services.AddMediatR(typeof(ServiceCollectionExtension));
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
