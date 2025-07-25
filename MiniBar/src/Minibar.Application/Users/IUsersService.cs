using Minibar.Contracts.Users;

namespace Minibar.Application.Users
{
    public interface IUsersService
    {
        Task<int> Create(CreateUserDTO userDTO, CancellationToken cancellationToken);

        Task<int> Login(LoginUserDTO userDTO, CancellationToken cancellationToken);

        Task<string> Logout(CancellationToken cancellationToken);
        Task<string> WhoIAm(CancellationToken cancellationToken);
    }
}
