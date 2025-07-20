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
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO, CancellationToken cancellationToken)
        {
            var userId = await _usersService.Create(createUserDTO, cancellationToken);
            return Ok(userId);
        }
    }
}
