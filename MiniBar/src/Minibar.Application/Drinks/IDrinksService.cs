using Minibar.Contracts.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksService
    {
        Task<int> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken);
    }
}