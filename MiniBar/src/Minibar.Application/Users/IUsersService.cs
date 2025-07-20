using Minibar.Contracts.Users;

namespace Minibar.Application.Users
{
    public interface IUsersService
    {
        Task<int> Create(CreateUserDTO userDTO, CancellationToken cancellationToken);
    }
}
