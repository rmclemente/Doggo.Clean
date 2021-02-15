using Doggo.Application.Interfaces;
using Doggo.Application.Services;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication;
using Doggo.Infra.CrossCutting.Messages.Notifications;
using Doggo.Infra.Data.Context;
using Doggo.Infra.Data.Repositories.Parametrizacao;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddRegisteredServices(this IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // Context
            services.AddScoped<DoggoCleanContext>();

            //Parametrizacao
            services.AddScoped<IBreedService, BreedService>();
            services.AddScoped<IBreedRepository, BreedRepository>();

            return services;
        }
    }
}
