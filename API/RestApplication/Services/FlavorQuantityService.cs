using Microsoft.EntityFrameworkCore;
using RestApplication.Models.Product;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class FlavorQuantityService
    {
        private readonly FlavorQuantityRepository repository;


        public FlavorQuantityService(FlavorQuantityRepository repository)
        {
            this.repository = repository;
        }


        public async Task<FlavorQuantityModel> GetById(Guid id)
        {
            return await repository.GetById(id);
        }


        public async Task<List<FlavorQuantityModel>> GetByProductId(Guid id)
        {
            return await repository.GetByProductId(id);
        }


        public async Task<List<FlavorQuantityModel>> GetByProductName(string productName)
        {
            return await repository.GetByProductName(productName);
        }


        public async Task<bool> DeleteById(Guid id)
        {
            return await repository.DeleteById(id);
        }
    }
}
