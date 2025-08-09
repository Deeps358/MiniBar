using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Drinks;
using Minibar.Application.Users;
using Minibar.Entities.Categories;
using Minibar.Entities.Roles;
using Minibar.Entities.Users;
using Minibar.Infrastructure.PostgreSQL.Repositories;

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
                if (dbContext.Database.EnsureCreated()) // создать БД с таблицами если их ещё нет
                {
                    dbContext.Categories.AddRange(
                        new Category { Name = "Текила" },
                        new Category { Name = "Водка" },
                        new Category { Name = "Джин" });

                    dbContext.Roles.AddRange(
                        new Role { RoleName = "Администратор" },
                        new Role { RoleName = "Модератор" },
                        new Role { RoleName = "Пользователь" });

                    dbContext.Users.AddRange(
                        new User("Admin", "AQAAAAIAAYagAAAAELC8Uemii1xKwGiENku2IZWxe9RTb5QF+4oeWy2lCq2i0mXgXPcgfW/upBmS3Gc9bg==" /*12345678*/, "hehe@email.ru", 1));

                    dbContext.SaveChanges();
                }
            }

            return serviceProvider;
        }
    }
}
