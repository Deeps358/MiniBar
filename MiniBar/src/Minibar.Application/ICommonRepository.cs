

namespace Minibar.Application
{
    public interface ICommonRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetAsync(int entityid, CancellationToken cancellationToken);

        Task<TEntity?> GetByNameAsync(string entityName, CancellationToken cancellationToken);

        Task<TEntity[]?> GetAllAsync(CancellationToken cancellationToken);

        Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<string> DeleteAsync(int entityId, CancellationToken cancellationToken);

    }
}
