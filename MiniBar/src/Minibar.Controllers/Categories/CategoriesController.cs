using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Categories;
using Minibar.Contracts.Categories;

namespace Minibar.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("GetById{categoryId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoriesService.GetByIdAsync(categoryId, cancellationToken);
            return Ok(category);
        }

        [HttpGet("GetFew")]
        public async Task<IActionResult> GetFew([FromQuery] int[] ids, CancellationToken cancellationToken)
        {
            if (ids == null || ids.Length == 0)
            {
                return BadRequest("Не переданы id категорий");
            }

            var categories = await _categoriesService.GetFewAsync(ids, cancellationToken);
            return Ok(categories);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _categoriesService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category added");
        }

        [HttpPut("{categoryId:int}")]
        [Authorize(Roles = "Администратор")] // Только пользователи с ролью "Admin"
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDTO updateCategoryDTO, CancellationToken cancellationToken)
        {
            return Ok("Category updated");
        }

        [HttpDelete("{categoryId:int}")]
        [Authorize(Roles = "Администратор")] // Только пользователи с ролью "Admin"
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            return Ok("Category removed");
        }
    }
}
