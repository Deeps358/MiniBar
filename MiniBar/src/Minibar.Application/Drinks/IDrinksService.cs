using CSharpFunctionalExtensions;
using Minibar.Contracts.Drinks;
using Minibar.Entities.Drinks;
using Shared;

namespace Minibar.Application.Drinks
{
    public interface IDrinksService : ICommonService<CreateDrinkDTO, Drink>
    {
        Task<Result<Drink[]?, Failure>> GetByGroupsAsync(string[] catNames, CancellationToken cancellationToken);
    }
}