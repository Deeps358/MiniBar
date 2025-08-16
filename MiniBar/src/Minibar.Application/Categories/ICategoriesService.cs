using CSharpFunctionalExtensions;
using Minibar.Entities.Categories;
using Shared;

namespace Minibar.Application.Categories
{
    public interface ICategoriesService
    {
        Task<Result<Category?, Failure>> GetByIdAsync(int categoryId, CancellationToken cancellationToken);

        Task<Result<Category[]?, Failure>> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken);

        Task<Result<Category[]?, Failure>> GetAllAsync(CancellationToken cancellationToken);
    }
}
