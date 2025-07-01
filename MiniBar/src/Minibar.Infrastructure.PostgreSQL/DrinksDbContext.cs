using Microsoft.EntityFrameworkCore;
using Minibar.Entities.Drinks;
using System;

namespace Minibar.Infrastructure.PostgreSQL
{
    public class DrinksDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; } // DbSet в efcore это реализация паттерна Repository и UnitOfWork(?)

        public DrinksDbContext(DbContextOptions<DrinksDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка модели базы данных, если необходимо
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Drink>().ToTable("Drinks");
        }
    }
}
