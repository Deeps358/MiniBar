using Microsoft.Extensions.Logging;
using Minibar.Entities.Categories;

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

        public async Task<int> CreateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<int> DeleteAsync(int categoryId, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetByIdAsync(categoryId, cancellationToken);
        }

        public async Task<Category[]?> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetFewAsync(categoryIds, cancellationToken);
        }

        public async Task<Category[]?> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAllAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
