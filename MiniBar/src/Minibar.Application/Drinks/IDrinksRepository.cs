using Minibar.Entities.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksRepository : ICommonRepository<Drink> // интерфейс для доступа к БД
    {
        Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<Drink[]?> GetAllAsync(CancellationToken cancellationToken);
    }
}
