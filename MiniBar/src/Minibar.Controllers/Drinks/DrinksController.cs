using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Drinks;
using Minibar.Contracts.Drinks;
using Minibar.Controllers.ResponceExtensions;

namespace Minibar.Controllers.Drinks
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinksService _drinksService;

        public DrinksController(IDrinksService drinksService)
        {
            _drinksService = drinksService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _drinksService.GetAllAsync(cancellationToken); // может прийти пустым если напитков нет в БД
            if(result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDTO createDrinkDTO, CancellationToken cancellationToken)
        {
            var result = await _drinksService.CreateAsync(createDrinkDTO, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponce();
            }

            return Ok(result.Value);
        }
    }
}
