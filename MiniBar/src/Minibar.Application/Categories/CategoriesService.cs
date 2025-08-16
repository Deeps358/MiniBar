using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minibar.Entities.Categories;
using Shared;

namespace Minibar.Application.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;
        private ILogger<CategoriesService> _logger;

        public CategoriesService(
            ICategoriesRepository categoriesRepository,
            ILogger<CategoriesService> logger)
        {
            _categoriesRepository = categoriesRepository;
            _logger = logger;
        }

        public async Task<Result<Category?, Failure>> GetByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetByIdAsync(categoryId, cancellationToken);
        }

        public async Task<Result<Category[]?, Failure>> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetFewAsync(categoryIds, cancellationToken);
        }

        public async Task<Result<Category[]?, Failure>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAllAsync(cancellationToken);
        }
    }
}
