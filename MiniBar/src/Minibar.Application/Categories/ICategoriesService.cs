using Minibar.Entities.Categories;

namespace Minibar.Application.Categories
{
    public interface ICategoriesService
    {
        Task<int> CreateAsync(Category category, CancellationToken cancellationToken);

        Task<int> UpdateAsync(Category category, CancellationToken cancellationToken);

        Task<int> DeleteAsync(int categoryId, CancellationToken cancellationToken);

        Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken);

        Task<Category[]?> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken);

        Task<Category[]?> GetAllAsync(CancellationToken cancellationToken);
    }
}
