using Domain.Interfaces;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), npgsql =>
                {
                    npgsql.EnableRetryOnFailure(10, TimeSpan.FromSeconds(15), null);
                }).UseSnakeCaseNamingConvention());

            services.AddScoped<IBreedRepository, BreedRepository>();

            return services;
        }
    }
}
