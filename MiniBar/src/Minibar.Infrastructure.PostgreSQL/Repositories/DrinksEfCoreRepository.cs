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

        public async Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken)
        {
            await _minibarDbContext.Drinks.AddAsync(drink, cancellationToken);
            await _minibarDbContext.SaveChangesAsync(cancellationToken);
            return drink.Id;
        }

        public async Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Drink[]?> GetAllAsync(CancellationToken cancellationToken)
        {
            Drink[] allDrinks = await _minibarDbContext.Drinks.ToArrayAsync(cancellationToken); // получить массив всех напитков
            return allDrinks;
        }

        public async Task<Drink?> GetByIdAsync(int drinkId, CancellationToken cancellationToken)
        {
            var drink = await _minibarDbContext.Drinks.FirstOrDefaultAsync(d => d.Id == drinkId, cancellationToken);
            return drink;
        }

        public async Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var drink = await _minibarDbContext.Drinks.FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
            return drink;
        }

        public async Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
