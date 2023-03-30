using Microsoft.EntityFrameworkCore;
using Polly;
using RestApplication.Models.Product;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class WeightPriceService
    {
        private readonly WeightPriceRepository repository;


        public WeightPriceService(WeightPriceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<WeightPriceModel> GetById(Guid id)
        {
            return await repository.GetById(id);
        }


        public async Task<List<WeightPriceModel>> GetByProductId(Guid id)
        {
            return await repository.GetByProductId(id);
        }


        public async Task<List<WeightPriceModel>> GetByProductName(string productName)
        {
            return await repository.GetByProductName(productName);
        }


        public async Task<bool> DeleteById(Guid id)
        {
            return await repository.DeleteById(id);
        }
    }
}
