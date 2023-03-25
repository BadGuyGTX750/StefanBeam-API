using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Product;

namespace RestApplication.Repositories
{
    public class ProductRepository
    {
        private readonly Entities dbContext;


        public ProductRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddProduct(ProductModel product)
        {
            try
            {
                string name = product.name;

                var prod = await dbContext.products.Where(u => u.name == name).FirstAsync();

                string sameName = null;

                if (prod != null)
                    sameName = prod.name;

                if (sameName != null)
                    return false;

                await dbContext.AddAsync(product);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<ProductModel> GetProductById(Guid id)
        {
            try
            {
                return await dbContext.products.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ProductModel> GetProductByName(string name)
        {
            try
            {
                return await dbContext.products.Where(u => u.name == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<ProductModel>> GetByCategoryName(string categoryName)
        { 
            try
            {
                return await dbContext.products.Where(u => u.categoryName == categoryName).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> UpdateProduct(ProductModel product)
        {
            try
            {
                string name = product.name;

                var prod = dbContext.products.Where(u => u.name == name).FirstOrDefault();

                string sameName = null;

                if (prod != null)
                    sameName = prod.name;

                if (sameName != null)
                    return false;

                dbContext.products.Update(product);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteProductById(Guid id)
        {
            try
            {
                var product = dbContext.products.Where(u => u.id == id).FirstOrDefault();
                dbContext.products.Remove(product);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductByName(string name)
        {
            try
            {
                var product = dbContext.products.Where(u => u.name == name).FirstOrDefault();
                dbContext.products.Remove(product);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
