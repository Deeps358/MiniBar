using Microsoft.EntityFrameworkCore;
using Minibar.Entities.Categories;
using Minibar.Entities.Drinks;
using Minibar.Entities.Roles;
using Minibar.Entities.Users;

namespace Minibar.Infrastructure.PostgreSQL
{
    public class MinibarDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; } // DbSet в efcore это реализация паттерна Repository и UnitOfWork(?)

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public MinibarDbContext(DbContextOptions<MinibarDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка модели базы данных, если необходимо
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Drink>().ToTable("Drinks");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Category>().ToTable("Categories");
        }
    }
}
