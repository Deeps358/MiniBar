using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application;
using Minibar.Application.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
        {
            services.AddWebDependencies(); // то, что было по умолчанию в Program
            services.AddApplication(); // это уже то что добавил я

            return services;
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
