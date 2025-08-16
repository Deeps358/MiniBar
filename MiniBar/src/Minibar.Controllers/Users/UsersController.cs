using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Users;
using Minibar.Contracts.Users;
using Minibar.Controllers.ResponceExtensions;

namespace Minibar.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
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
            var result = await _usersService.CreateAsync(createUserDTO, cancellationToken);

            if(result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            var result = await _usersService.Login(loginUserDTO, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }

        [HttpGet("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var result = await _usersService.Logout(cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }

        [HttpGet("WhoIAm")]
        [Authorize]
        public async Task<IActionResult> WhoIAm(CancellationToken cancellationToken)
        {
            var result = await _usersService.WhoIAm(cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }
    }
}
