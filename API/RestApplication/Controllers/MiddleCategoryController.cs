using Microsoft.AspNetCore.Mvc;
using RestApplication.Models.Category;
using RestApplication.Repositories;

namespace RestApplication.Controllers
{
    public class MiddleCategoryController : Controller
    {
        private readonly MiddleCategoryService service;
        private readonly TopCategoryService topCategoryService;


        public MiddleCategoryController(
            MiddleCategoryService service,
            TopCategoryService topCategoryService
            )
        {
            this.service = service;
            this.topCategoryService = topCategoryService;
        }


        [HttpPost("/api/middleCategory/add")]
        public async Task<IActionResult> AddMiddleCategory([FromBody] MiddleCategoryModel category)
        {
            var name = category.name;
            var parentCategoryName = category.parentCategoryName;

            // check if any of the required parameters are null
            if (name == null)
                return BadRequest();

            if (parentCategoryName == null)
                return BadRequest();

            // check if a category with the same name already exists
            var category_ = await service.GetMiddleCategoryByName(name);
            if (category_ != null)
                return BadRequest();

            // check if it exists a parent category
            var parentCategory = await topCategoryService.GetTopCategoryByName(parentCategoryName);
            if (parentCategory == null)
                return BadRequest();

            // create the new category
            var categoryToAdd = new MiddleCategoryModel();
            categoryToAdd.name = category.name;
            categoryToAdd.parentCategoryId = parentCategory.id;
            categoryToAdd.parentCategoryName = parentCategoryName;

            // add the new category
            if (!await service.AddMiddleCategory(categoryToAdd))
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpDelete("/api/middleCategory/delete")]
        public async Task<IActionResult> DeleteMiddleCategoryByName([FromQuery] string name)
        {
            // check if name is not null
            if (name == null)
                return BadRequest();

            // check if a it exists an instance with this name
            var inst = await service.GetMiddleCategoryByName(name);
            if (inst == null)
                return NotFound();

            // restricted deletion
            if (!await service.DeleteMiddleCategoryByName(name))
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
