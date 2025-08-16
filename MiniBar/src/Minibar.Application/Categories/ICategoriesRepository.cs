using Minibar.Entities.Categories;

namespace Minibar.Application.Categories
{
    public interface ICategoriesRepository : ICommonRepository<Category>
    {
        Task<Category[]?> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken);

        Task<Category[]?> GetAllAsync(CancellationToken cancellationToken);
    }
}
