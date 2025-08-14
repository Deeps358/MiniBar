using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minibar.Application.Categories;
using Minibar.Application.Drinks;
using Minibar.Application.Users;
using Minibar.Entities.Categories;
using Minibar.Entities.Drinks;
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
            services.AddScoped<ICategoriesRepository, CategoriesEfCoreRepository>();

            return services;
        }

        public static IServiceProvider AddCreatingDb(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MinibarDbContext>();
                if (dbContext.Database.EnsureCreated()) // создать БД с таблицами если их ещё нет
                {
                    dbContext.Drinks.AddRange(
                        new Drink("Green baboon", "Хороший Питерский джин", null, 1, 3, null),
                        new Drink("Olmeca gold", "Текилка, дороговато", "/drinks/Olmeca Gold.jpg", 1, 1, null),
                        new Drink("Indiana Gold", "Отличная текила", "/drinks/Indiana Gold.jpg", 1, 1, null),
                        new Drink("Кагорчик", "хехехе", null, 1, 3, null),
                        new Drink("Beluga", "Не ну это Белуга, премиум водка", null, 1, 2, null));

                    dbContext.Categories.AddRange(
                        new Category { Name = "Текила" },
                        new Category { Name = "Водка" },
                        new Category { Name = "Джин" },
                        new Category { Name = "Всё остальное" });

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
