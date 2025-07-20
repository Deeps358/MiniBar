using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Drinks;
using Minibar.Application.Users;
using Minibar.Infrastructure.PostgreSQL.Repositories;
using System;

namespace Minibar.Infrastructure.PostgreSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MinibarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

            services.AddScoped<IDrinksRepository, DrinksEfCoreRepository>();
            services.AddScoped<IUsersRepository, UsersEfCoreRepository>();

            return services;
        }

        public static IServiceProvider AddCreatingDb(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MinibarDbContext>();
                dbContext.Database.EnsureCreated();
            }

            return serviceProvider;
        }
    }
}
