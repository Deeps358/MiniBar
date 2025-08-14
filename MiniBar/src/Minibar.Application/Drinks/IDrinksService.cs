using CSharpFunctionalExtensions;
using Minibar.Contracts.Drinks;
using Minibar.Entities.Drinks;
using Shared;

namespace Minibar.Application.Drinks
{
    public interface IDrinksService
    {
        Task<Result<int, Failure>> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken);

        Task<Drink[]> GetAll(CancellationToken cancellationToken);
    }
}