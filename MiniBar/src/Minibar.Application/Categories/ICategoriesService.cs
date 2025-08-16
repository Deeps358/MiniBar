using CSharpFunctionalExtensions;
using Minibar.Contracts.Categories;
using Minibar.Entities.Categories;
using Shared;

namespace Minibar.Application.Categories
{
    public interface ICategoriesService : ICommonService<CreateCategoryDTO, Category>
    {
        Task<Result<Category[]?, Failure>> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken);

        Task<Result<Category[]?, Failure>> GetAllAsync(CancellationToken cancellationToken);
    }
}
