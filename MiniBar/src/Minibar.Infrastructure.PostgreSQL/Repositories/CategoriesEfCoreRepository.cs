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

        public async Task<int> CreateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<int> DeleteAsync(int categoryId, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<Category[]?> GetAllAsync(CancellationToken cancellationToken)
        {
            Category[] allCategories = await _minibarDbContext.Categories.ToArrayAsync(cancellationToken); // получить массив всех категорий
            return allCategories;
        }

        public async Task<Category[]?> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken)
        {
            Category[] fewCategories = await _minibarDbContext.Categories
                .Where(c => categoryIds.Contains(c.Id)).ToArrayAsync(cancellationToken);
            return fewCategories;
        }

        public async Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _minibarDbContext.Categories.FirstOrDefaultAsync(d => d.Id == categoryId, cancellationToken);
            return category;
        }

        public async Task<int> UpdateAsync(Category category, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
