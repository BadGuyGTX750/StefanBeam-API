using RestApplication.Data;
using RestApplication.Models.Category;
using RestApplication.Repositories;

namespace RestApplication.Repositories
{
    public class TopCategoryService
    {
        private readonly TopCategoryRepository repository;


        public TopCategoryService(TopCategoryRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> AddTopCategory(TopCategoryModel category)
        {
            return await repository.AddTopCategory(category);
        }


        public async Task<TopCategoryModel> GetTopCategoryById(Guid id)
        {
            return await repository.GetTopCategoryById(id);
        }


        public async Task<TopCategoryModel> GetTopCategoryByName(string name)
        {
            return await repository.GetTopCategoryByName(name);
        }


        public async Task<List<TopCategoryModel>> GetAll()
        {
            return await repository.GetAll();
        }


        public async Task<bool> DeleteTopCategoryById(Guid id)
        {
            return await repository.DeleteTopCategoryById(id);
        }


        public async Task<bool> DeleteTopCategoryByName(string name)
        {
            return await repository.DeleteTopCategoryByName(name);
        }
    }
}
