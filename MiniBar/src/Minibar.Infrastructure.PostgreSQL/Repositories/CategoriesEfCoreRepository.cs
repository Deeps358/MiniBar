using Microsoft.EntityFrameworkCore;
using Minibar.Application.Categories;
using Minibar.Entities.Categories;

namespace Minibar.Infrastructure.PostgreSQL.Repositories
{
    public class CategoriesEfCoreRepository : ICategoriesRepository
    {
        private readonly MinibarDbContext _minibarDbContext;

        public CategoriesEfCoreRepository(MinibarDbContext minibarDbContext)
        {
            _minibarDbContext = minibarDbContext;
        }

        public async Task<Category?> GetAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _minibarDbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
            return category;
        }

        public async Task<Category?> GetByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            var category = await _minibarDbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName, cancellationToken);
            return category;
        }

        public async Task<Category[]?> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken)
        {
            Category[] fewCategories = await _minibarDbContext.Categories
                .Where(c => categoryIds.Contains(c.Id)).ToArrayAsync(cancellationToken);
            return fewCategories;
        }

        public async Task<Category[]?> GetAllAsync(CancellationToken cancellationToken)
        {
            Category[] allCategories = await _minibarDbContext.Categories.ToArrayAsync(cancellationToken); // получить массив всех категорий
            return allCategories;
        }

        public async Task<int> CreateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<int> UpdateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<string> DeleteAsync(int categoryId, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
