using Microsoft.AspNetCore.Authentication.Cookies;
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
            services.AddCookieAuth(); // куки аутентификация

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

        private static IServiceCollection AddCookieAuth(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Точное время жизни 30 минут
                    options.SlidingExpiration = false; // Отключаем автопродление

                    // Полностью отключаем редиректы для API
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsJsonAsync(new
                            {
                                error = "Unauthorized",
                                message = "Сначала залогиньтесь",
                            });
                        },
                        OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsJsonAsync(new
                            {
                                error = "Forbidden",
                                message = "Тебе сюда нельзя",
                            });
                        },
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Администратор"));
            });

            services.AddHttpContextAccessor(); // контекст для авторизации

            return services;
        }
    }
}
