using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Categories;
using Minibar.Application.Drinks;
using Minibar.Application.Users;

namespace Minibar.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IDrinksService, DrinksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            return services;
        }
    }
}
