using CSharpFunctionalExtensions;
using Minibar.Contracts.Users;
using Minibar.Entities.Users;
using Shared;

namespace Minibar.Application.Users
{
    public interface IUsersService : ICommonService<CreateUserDTO, User>
    {
        Task<Result<int, Failure>> Login(LoginUserDTO userDTO, CancellationToken cancellationToken);

        Task<Result<string, Failure>> Logout(CancellationToken cancellationToken);

        Task<Result<string, Failure>> WhoIAm(CancellationToken cancellationToken);
    }
}
