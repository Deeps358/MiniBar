using Microsoft.AspNetCore.Mvc;
using Minibar.Application.Categories;
using Minibar.Controllers.ResponceExtensions;

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

            if(category.IsFailure)
            {
                return category.Error.ToResponce();
            }

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

            if(categories.IsFailure)
            {
                return categories.Error.ToResponce();
            }

            return Ok(categories);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _categoriesService.GetAllAsync(cancellationToken);

            if (categories.IsFailure)
            {
                return categories.Error.ToResponce();
            }

            return Ok(categories);
        }
    }
}
