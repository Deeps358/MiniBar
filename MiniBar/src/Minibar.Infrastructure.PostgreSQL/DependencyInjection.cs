using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Drinks;
using Minibar.Infrastructure.PostgreSQL.Repositories;

namespace Minibar.Infrastructure.PostgreSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DrinksDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDrinksRepository, DrinksEfCoreRepository>();

            return services;
        }
    }
}
