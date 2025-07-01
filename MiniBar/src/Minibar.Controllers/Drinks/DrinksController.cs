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

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] FindDrinkDTO getDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink found");
        }

        [HttpGet("{drinkId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink got, hold it");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDTO createDrinkDTO, CancellationToken cancellationToken)
        {
            var drinkId = await _drinksService.Create(createDrinkDTO, cancellationToken);
            return Ok(drinkId);
        }

        [HttpPut("{drinkId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid drinkId, [FromBody] UpdateDrinkDTO updateDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink updated");
        }

        [HttpDelete("{drinkId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink removed");
        }
    }
}
