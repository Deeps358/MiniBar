using CSharpFunctionalExtensions;
using Shared;

namespace Minibar.Application
{
    public interface ICommonService<TDto, TEntity>
        where TEntity : class
    {
        public Task<Result<TEntity?, Failure>> GetAsync(int id, CancellationToken cancellationToken);

        public Task<Result<TEntity?, Failure>> GetByNameAsync(string name, CancellationToken cancellationToken);

        public Task<Result<TEntity[]?, Failure>> GetAllAsync(CancellationToken cancellationToken);

        public Task<Result<int, Failure>> CreateAsync(TDto dto, CancellationToken cancellationToken);

        public Task<Result<int, Failure>> UpdateAsync(TDto dto, CancellationToken cancellationToken);

        public Task<Result<string, Failure>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
