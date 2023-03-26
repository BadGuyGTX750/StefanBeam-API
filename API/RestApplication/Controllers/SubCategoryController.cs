using Microsoft.AspNetCore.Mvc;
using RestApplication.Models.Category;
using RestApplication.Repositories;

namespace RestApplication.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly SubCategoryService service;


        public SubCategoryController(
            SubCategoryService service
            )
        {
            this.service = service;
        }


        [HttpPost("/api/subCategory/add")]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCategoryModel category)
        {
            var name = category.name;

            // check if any of the required parameters are null
            if (name == null)
                return BadRequest();

            // check if a category with the same name already exists
            var category_ = await service.GetSubCategoryByName(name);
            if (category_ != null)
                return BadRequest();

            // create the new category
            var categoryToAdd = new SubCategoryModel();
            categoryToAdd.name = category.name;

            // add the new category
            if (!await service.AddSubCategory(categoryToAdd))
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpDelete("/api/subCategory/delete")]
        public async Task<IActionResult> DeleteSubCategoryByName([FromQuery] string name)
        {
            // check if name is not null
            if (name == null)
                return BadRequest();

            // check if a it exists an instance with this name
            var inst = await service.GetSubCategoryByName(name);
            if (inst == null)
                return NotFound();

            // restricted deletion
            if (!await service.DeleteSubCategoryByName(name))
            {
                return StatusCode(500);
            }

            return Ok();
        }

    }
}
