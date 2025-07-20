using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Drinks;
using Minibar.Contracts.Drinks;

namespace Minibar.Controllers.Drinks
{
    [ApiController]
    [Route("[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinksService _drinksService;

        public DrinksController(IDrinksService drinksService)
        {
            _drinksService = drinksService;
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find([FromQuery] FindDrinkDTO getDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink found");
        }

        [HttpGet("GetById{drinkId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink got, hold it");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDTO createDrinkDTO, CancellationToken cancellationToken)
        {
            var drinkId = await _drinksService.Create(createDrinkDTO, cancellationToken);
            return Ok(drinkId);
        }

        [HttpPut("Update{drinkId:int}")]
        public async Task<IActionResult> Update([FromRoute] Guid drinkId, [FromBody] UpdateDrinkDTO updateDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink updated");
        }

        [HttpDelete("Delete{drinkId:int}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink removed");
        }
    }
}
