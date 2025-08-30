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

        [HttpGet("GetByGroupNames")]
        public async Task<IActionResult> GetByGroupNames([FromQuery] string[] catNames, CancellationToken cancellationToken)
        {
            if (catNames == null || catNames.Length == 0)
            {
                return BadRequest("Не передано название категории!");
            }

            var drinks = await _drinksService.GetByGroupsAsync(catNames, cancellationToken);

            if (drinks.IsFailure)
            {
                return drinks.Error.ToResponce();
            }

            return Ok(drinks.Value);
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
        //[Authorize]
        public async Task<IActionResult> Create([FromForm] CreateDrinkDTO createDrinkDTO, CancellationToken cancellationToken)
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
