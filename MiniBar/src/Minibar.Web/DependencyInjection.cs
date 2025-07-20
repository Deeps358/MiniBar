using Minibar.Application;
using Minibar.Infrastructure.PostgreSQL;

namespace Minibar.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProgramDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddWebDependencies(); // то, что было по умолчанию в Program
            services.AddApplication(); // это уже то что добавил я
            services.AddPostgresInfrastructure(configuration); // сервис для БД

            return services;
        }

        public static IServiceProvider AddBuildDependencies(this IServiceProvider serviceProvider)
        {
            serviceProvider.AddCreatingDb();
            return serviceProvider;
        }

        private static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            return services;
        }
    }
}
