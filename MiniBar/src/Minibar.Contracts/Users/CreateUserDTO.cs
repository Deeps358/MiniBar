namespace Minibar.Contracts.Users
{
    // дто для передачи в контроллер
    public record CreateUserDTO(string UserName, string Password, string Email, int RoleId);
}
