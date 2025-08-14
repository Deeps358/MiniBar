using Minibar.Entities.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksRepository // интерфейс для доступа к БД
    {
        Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken);

        Task<int> UpdateAsync(Drink drink, CancellationToken cancellationToken);

        Task<int> DeleteAsync(int drinkId, CancellationToken cancellationToken);

        Task<Drink?> GetByIdAsync(int drinkId, CancellationToken cancellationToken);

        Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<Drink[]?> GetAllAsync(CancellationToken cancellationToken);
    }
}
