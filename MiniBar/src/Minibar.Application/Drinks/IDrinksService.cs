using Minibar.Contracts.Drinks;
using Minibar.Entities.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksService
    {
        Task<int> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken);

        Task<Drink[]> GetAll(CancellationToken cancellationToken);
    }
}