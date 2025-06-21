using Microsoft.AspNetCore.Mvc;
using Minibar.Contracts.Categories;

namespace Minibar.Controllers.Categories
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category got");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category added");
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDTO updateCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category updated");
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category removed");
        }
    }
}
