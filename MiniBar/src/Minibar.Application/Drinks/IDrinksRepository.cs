using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minibar.Entities.Drinks;

namespace Minibar.Application.Drinks
{
    public interface IDrinksRepository // интерфейс для доступа к БД
    {
        Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken);

        Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken);

        Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken);

        Task<Drink?> GetByIdAsync(int drinkId, CancellationToken cancellationToken);

        Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<Drink[]?> GetAllAsync(CancellationToken cancellationToken);
    }
}
