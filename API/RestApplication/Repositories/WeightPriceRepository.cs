using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Product;

namespace RestApplication.Repositories
{
    public class WeightPriceRepository
    {
        private Entities dbContext;


        public WeightPriceRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<WeightPriceModel> GetById(Guid id)
        {   
            try
            {
                return await dbContext.weightPrices.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<WeightPriceModel>> GetByProductId(Guid id)
        {
            try
            {
                return await dbContext.weightPrices.Where(u => u.productId == id).ToListAsync();
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
