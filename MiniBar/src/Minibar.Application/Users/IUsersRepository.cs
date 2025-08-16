using Minibar.Entities.Users;

namespace Minibar.Application.Users
{
    public interface IUsersRepository : ICommonRepository<User>
    {
        Task<string> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken);
    }
}
