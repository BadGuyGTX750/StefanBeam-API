using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using RestApplication.Models.Product;
using RestApplication.Repositories;
using RestApplication.Services;

namespace RestApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService service;
        private readonly SubCategoryService subCategoryService;


        public ProductController(ProductService service,
            SubCategoryService subCategoryService)
        {
            this.service = service;
            this.subCategoryService = subCategoryService;
        }


        [HttpPost("/api/product/add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel product)
        {
            var name = product.name;
            var shortDescr = product.shortDescr;
            var longDescr = product.longDescr;
            var categoryName = product.categoryName;
            var weight_price = product.weight_price;
            var flavor_quantity = product.flavor_quantity;

            // check if any of the required parameters == null
            if (name == null || shortDescr == null || longDescr == null ||
                categoryName == null || weight_price == null ||
                flavor_quantity == null) 
                return BadRequest();

            // check if a product with the same name already exists
            if (await service.GetProductByName(name) != null)
                return BadRequest();

            // check if the mentioned categories exists
            var subCategory = await subCategoryService.GetSubCategoryByName(categoryName);
            if (subCategory == null)
                return BadRequest();

            ProductModel productToAdd = new ProductModel();

            productToAdd.name = name;
            productToAdd.shortDescr = shortDescr;
            productToAdd.longDescr = longDescr;
            //productToAdd.parentCategoryIds = subCategoryIds;
            productToAdd.categoryName = categoryName;
            productToAdd.weight_price = weight_price;
            productToAdd.flavor_quantity = flavor_quantity;
            productToAdd.isInStock= this.IsInStock(flavor_quantity);

            if (!await service.AddProduct(productToAdd))
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpDelete("/api/product/delete")]
        public async Task<IActionResult> DeleteProductByName([FromQuery] string name)
        {
            // check if name is not null
            if (name == null)
                return BadRequest();

            // chekc if a it exists an instance with this name
            var inst = await service.GetProductByName(name);
            if (inst == null)
                return NotFound();

            // cascade deletion of a product along with weightPrices and flavorQuantities   
            if (!await service.DeleteProductByName(name))
            {
                return StatusCode(500);
            }

            return Ok();
        }


        private bool IsInStock(List<FlavorQuantityModel> flavorQuantity)
        {
            uint sum = 0;

            // check the stock for each flavour and sum them up
            foreach (var item in flavorQuantity)
            {
                sum += item.quantity;
            }

            // check if there is any product available
            if (sum == 0)
                return false;

            return true;
        }
    }
}
