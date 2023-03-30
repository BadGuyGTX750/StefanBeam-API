using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Product;

namespace RestApplication.Repositories
{
    public class FlavorQuantityRepository
    {
        private Entities dbContext;

        public FlavorQuantityRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<FlavorQuantityModel> GetById(Guid id)
        {
            try
            {
                return await dbContext.flavorQuantities.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<FlavorQuantityModel>> GetByProductId(Guid id)
        {
            try
            {
                return await dbContext.flavorQuantities.Where(u => u.productId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<FlavorQuantityModel>> GetByProductName(string productName)
        {
            try
            {
                return await dbContext.flavorQuantities.Where(u => u.productName == productName).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> DeleteById(Guid id)
        {
            try
            {
                var toDelete = dbContext.products.Where(u => u.id == id).FirstOrDefault();
                dbContext.products.Remove(toDelete);
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
