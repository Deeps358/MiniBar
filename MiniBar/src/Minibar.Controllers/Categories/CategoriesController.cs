using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minibar.Contracts.Categories;

namespace Minibar.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category got");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category added");
        }

        [HttpPut("{categoryId:guid}")]
        [Authorize(Roles = "Администратор")] // Только пользователи с ролью "Admin"
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDTO updateCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category updated");
        }

        [HttpDelete("{categoryId:guid}")]
        [Authorize(Roles = "Администратор")] // Только пользователи с ролью "Admin"
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category removed");
        }
    }
}
