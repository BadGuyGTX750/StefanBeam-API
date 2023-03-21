using Microsoft.AspNetCore.Mvc;
using RestApplication.Models.Category;
using RestApplication.Repositories;

namespace RestApplication.Controllers
{
    public class TopCategoryController : Controller
    {
        private readonly TopCategoryService service;


        public TopCategoryController(TopCategoryService service)
        {
            this.service = service;
        }


        [HttpPost("/api/topCategory/add")]
        public async Task<IActionResult> AddTopCategory([FromBody] TopCategoryModel category)
        {
            var name = category.name;

            // check if any of the required parameters are null
            if (name == null)
                return BadRequest();

            // check if a category with the same name already exists
            var category_ = await service.GetTopCategoryByName(name);
            if (category_ != null)
                return BadRequest();

            // create the new category
            var categoryToAdd = new TopCategoryModel();
            categoryToAdd.name = category.name;

            // add the new category
            if (!await service.AddTopCategory(categoryToAdd))
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpDelete("/api/topCategory/delete")]
        public async Task<IActionResult> DeleteTopCategoryByName([FromQuery] string name)
        {
            // check if name is not null
            if (name == null)
                return BadRequest();

            // check if a it exists an instance with this name
            var inst = await service.GetTopCategoryByName(name);
            if (inst == null)
                return NotFound();

            // restricted deletion
            if (!await service.DeleteTopCategoryByName(name))
            {
                return StatusCode(500);
            }
                
            return Ok();
        }


        [HttpGet("/api/topCategory/getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAll());
        }

    }
}
