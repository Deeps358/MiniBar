using Microsoft.EntityFrameworkCore;
using Minibar.Application.Drinks;
using Minibar.Entities.Drinks;

namespace Minibar.Infrastructure.PostgreSQL.Repositories
{
    public class DrinksEfCoreRepository : IDrinksRepository
    {
        private readonly DrinksDbContext _drinksDbContext;

        public DrinksEfCoreRepository(DrinksDbContext drinksDbContext)
        {
            _drinksDbContext = drinksDbContext;
        }

        public async Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken)
        {
            await _drinksDbContext.Drinks.AddAsync(drink, cancellationToken);
            await _drinksDbContext.SaveChangesAsync(cancellationToken);
            return drink.id;
        }

        public async Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Drink?> GetByIdAsync(int drinkId, CancellationToken cancellationToken)
        {
            var drink = await _drinksDbContext.Drinks.FirstOrDefaultAsync(d => d.id == drinkId, cancellationToken);
            return drink;
        }

        public async Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var drink = await _drinksDbContext.Drinks.FirstOrDefaultAsync(d => d.name == name, cancellationToken);
            return drink;
        }

        public async Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
