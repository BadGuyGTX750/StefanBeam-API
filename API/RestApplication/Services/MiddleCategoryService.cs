using RestApplication.Data;
using RestApplication.Models.Category;

namespace RestApplication.Repositories
{
    public class MiddleCategoryService
    {
        private readonly MiddleCategoryRepository repository;


        public MiddleCategoryService(MiddleCategoryRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> AddMiddleCategory(MiddleCategoryModel category)
        {
            return await repository.AddMiddleCategory(category);
        }


        public async Task<MiddleCategoryModel> GetMiddleCategoryById(Guid id)
        {
            return await repository.GetMiddleCategoryById(id);
        }


        public async Task<MiddleCategoryModel> GetMiddleCategoryByName(string name)
        {
            return await repository.GetMiddleCategoryByName(name);
        }


        public async Task<bool> AssignToTopCategory(SubCategoryModel middleCategory, MiddleCategoryModel topCategory)
        {
            return await repository.AssignToTopCategory(middleCategory, topCategory);
        }


        public async Task<bool> DeleteMiddleCategoryById(Guid id)
        {
            return await repository.DeleteMiddleCategoryById(id);
        }


        public async Task<bool> DeleteMiddleCategoryByName(string name)
        {
            return await repository.DeleteMiddleCategoryByName(name);
        }
    }
}
