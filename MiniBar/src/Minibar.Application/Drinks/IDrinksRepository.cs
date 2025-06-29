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
        Task<Guid> CreateAsync(Drink drink, CancellationToken cancellationToken);

        Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken);

        Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken);

        Task<Drink?> GetByIdAsync(Guid drinkId, CancellationToken cancellationToken);
    }
}
