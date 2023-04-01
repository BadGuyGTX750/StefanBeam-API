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
            var parentName = category.parentCategoryName;

            // check if any of the required parameters are null
            if (name == null)
                return BadRequest();

            // check if a category with the same name already exists
            var category_ = await service.GetSubCategoryByName(name);
            if (category_ != null)
                return BadRequest();


            var pCategS = new SubCategoryModel();
            // check if the parent category exists
            if (!(parentName == "string" || parentName == "" || parentName == null))
            {
                var pCateg = await service.GetSubCategoryByName(parentName);
                if (pCateg == null)
                    return BadRequest("No such parent category");

                // mark the isBottom property as false
                pCategS = pCateg;
                pCategS.isBottom = false;
            }
            

            // create the new category
            var categoryToAdd = new SubCategoryModel();
            categoryToAdd.name = category.name;
            if (parentName == "string" || parentName == "" || parentName == null)
                categoryToAdd.parentCategoryName = null;
            else
                categoryToAdd.parentCategoryName = parentName;

            // add the new category
            if (!await service.AddSubCategory(categoryToAdd))
            {
                return StatusCode(500);
            }

            // update the parent category to have isBotom to false from earlier
            if (pCategS.id != Guid.Empty)
            {
                if (!await service.UpdateSubCategory(pCategS))
                {
                    return StatusCode(500);
                }
            }
            
            return Ok();
        }


        [HttpGet("/api/subCategory/getByName")]
        public async Task<IActionResult> GetSubCategoryByName([FromQuery] string name)
        {
            if (name == null)
                return BadRequest();

            var subc = await service.GetSubCategoryByName(name);

            if (subc == null)
                return NotFound();
            
            return Ok(subc);
        }


        [HttpGet("/api/subCategory/getByParentName")]
        public async Task<IActionResult> GetSubCategoryByParentName([FromQuery] string parentName)
        {
            if (parentName == null)
                return BadRequest();

            var subc = await service.GetSubCategoryByParentName(parentName);

            if (subc == null || !subc.Any())
                return NotFound();

            return Ok(subc);
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

            var parentName = inst.parentCategoryName;

            // restricted deletion
            if (!await service.DeleteSubCategoryByName(name))
            {
                return StatusCode(500);
            }

            var categs = await service.GetSubCategoryByParentName(parentName);
            if (categs == null || !categs.Any())
            {
                var parentCateg = await service.GetSubCategoryByName(parentName);
                parentCateg.isBottom = true;
                await service.UpdateSubCategory(parentCateg);
            }

            return Ok();
        }

    }
}
