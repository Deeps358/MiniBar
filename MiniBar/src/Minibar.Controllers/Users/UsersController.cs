using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Users;
using Minibar.Contracts.Users;

namespace Minibar.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Администратор")] // Только пользователи с ролью "Admin"
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO, CancellationToken cancellationToken)
        {
            var userId = await _usersService.Create(createUserDTO, cancellationToken);
            return Ok(userId);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            var userId = await _usersService.Login(loginUserDTO, cancellationToken);
            return Ok(userId);
        }

        [HttpGet("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            return Ok(await _usersService.Logout(cancellationToken));
        }

        [HttpGet("WhoIAm")]
        [Authorize]
        public async Task<IActionResult> WhoIAm(CancellationToken cancellationToken)
        {
            return Ok(await _usersService.WhoIAm(cancellationToken));
        }
    }
}
