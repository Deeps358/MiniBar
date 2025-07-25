using Microsoft.EntityFrameworkCore;
using Minibar.Application.Users;
using Minibar.Entities.Users;

namespace Minibar.Infrastructure.PostgreSQL.Repositories
{
    public class UsersEfCoreRepository : IUsersRepository
    {
        private readonly MinibarDbContext _minibarDbContext;

        public UsersEfCoreRepository(MinibarDbContext minibarDbContext)
        {
            _minibarDbContext = minibarDbContext;
        }

        public async Task<int> CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _minibarDbContext.Users.AddAsync(user, cancellationToken);
            await _minibarDbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<int> DeleteAsync(int userId, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await _minibarDbContext.Users.FirstOrDefaultAsync(d => d.UserName == userName, cancellationToken);
            return user;
        }

        public async Task<string> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _minibarDbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
            return role.RoleName;
        }
    }
}
