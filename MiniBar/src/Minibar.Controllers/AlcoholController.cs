using Microsoft.AspNetCore.Mvc;
using Minibar.Contracts;

namespace Minibar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlcoholController : ControllerBase
    {
        [HttpGet("drinks")]
        public async Task<IActionResult> Find([FromQuery] FindDrinkDTO getDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink found");
        }

        [HttpGet("drinks/{drinkId:guid}")]
        public async Task<IActionResult> GetDrinkById([FromRoute] Guid drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink got");
        }

        [HttpPost("drinks")]
        public async Task<IActionResult> CreateDrink([FromBody] CreateDrinkDTO createDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink added");
        }

        [HttpPut("drinks/{drinkId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid drinkId, [FromBody] UpdateDrinkDTO updateDrinkDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink updated");
        }

        [HttpDelete("drinks/{drinkId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drinkId, CancellationToken cancellationToken)
        {
            return Ok("Drink removed");
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category got");
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category added");
        }

        [HttpPut("category/{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] CreateCategoryDTO updateCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Drink updated");
        }

        [HttpDelete("category/{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Drink removed");
        }
    }
}
