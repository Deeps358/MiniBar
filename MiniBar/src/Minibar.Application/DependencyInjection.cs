using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IDrinksService, DrinksService>();

            return services;
        }
    }
}
