using Minibar.Entities.Users;

namespace Minibar.Application.Users
{
    public interface IUsersRepository
    {
        Task<int> CreateAsync(User user, CancellationToken cancellationToken);

        Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken);

        Task<int> DeleteAsync(int userId, CancellationToken cancellationToken);

        Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
