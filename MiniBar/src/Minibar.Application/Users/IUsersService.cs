using CSharpFunctionalExtensions;
using Minibar.Contracts.Users;
using Shared;

namespace Minibar.Application.Users
{
    public interface IUsersService
    {
        Task<Result<int, Failure>> CreateAsync(CreateUserDTO userDTO, CancellationToken cancellationToken);

        Task<Result<int, Failure>> Login(LoginUserDTO userDTO, CancellationToken cancellationToken);

        Task<Result<string, Failure>> Logout(CancellationToken cancellationToken);

        Task<Result<string, Failure>> WhoIAm(CancellationToken cancellationToken);
    }
}
