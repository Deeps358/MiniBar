using Microsoft.EntityFrameworkCore;
using Minibar.Application.Drinks;
using Minibar.Entities.Drinks;

namespace Minibar.Infrastructure.PostgreSQL.Repositories
{
    public class DrinksEfCoreRepository : IDrinksRepository
    {
        private readonly MinibarDbContext _minibarDbContext;

        public DrinksEfCoreRepository(MinibarDbContext minibarDbContext)
        {
            _minibarDbContext = minibarDbContext;
        }

        public async Task<Drink?> GetAsync(int drinkId, CancellationToken cancellationToken)
        {
            var drink = await _minibarDbContext.Drinks.FirstOrDefaultAsync(d => d.Id == drinkId, cancellationToken);
            return drink;
        }

        public async Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var drink = await _minibarDbContext.Drinks.FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
            return drink;
        }

        public async Task<Drink[]?> GetByGroupsAsync(int[] catNames, CancellationToken cancellationToken)
        {
            var drinks = await _minibarDbContext.Drinks.Where(d => catNames.Contains(d.CategoryId)).ToArrayAsync(cancellationToken);
            return drinks;
        }

        public async Task<Drink[]?> GetAllAsync(CancellationToken cancellationToken)
        {
            Drink[] allDrinks = await _minibarDbContext.Drinks.ToArrayAsync(cancellationToken); // получить массив всех напитков
            return allDrinks;
        }

        public async Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken)
        {
            await _minibarDbContext.Drinks.AddAsync(drink, cancellationToken);
            await _minibarDbContext.SaveChangesAsync(cancellationToken);
            return drink.Id;
        }

        public async Task<int> UpdateAsync(Drink drink, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<string> DeleteAsync(int drinkId, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
