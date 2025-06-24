using Minibar.Contracts.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksService
    {
        Task<Guid> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken);
    }
}