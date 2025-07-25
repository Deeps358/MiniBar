using System.ComponentModel.DataAnnotations;

namespace Minibar.Contracts.Users
{
    public record LoginUserDTO(
        string UserName,
        string Password);
}
