using Microsoft.EntityFrameworkCore;
using RestApplication.Models.Product;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class ProductService
    {
        private readonly ProductRepository repository;


        public ProductService(ProductRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> AddProduct(ProductModel product)
        {
            return await repository.AddProduct(product);
        }


        public async Task<ProductModel> GetProductById(Guid id)
        {
            return await repository.GetProductById(id);
        }


        public async Task<ProductModel> GetProductByName(string name)
        {
            return await repository.GetProductByName(name);
        }


        public async Task<List<ProductModel>> GetByCategoryName(string categoryName)
        {
            return await repository.GetByCategoryName(categoryName);
        }


        public async Task<bool> UpdateProduct(ProductModel product)
        {
            return await repository.UpdateProduct(product);
        }


        public async Task<bool> DeleteProductById(Guid id)
        {
            return await repository.DeleteProductById(id);
        }


        public async Task<bool> DeleteProductByName(string name)
        {
            return await repository.DeleteProductByName(name);
        }

    }
}
