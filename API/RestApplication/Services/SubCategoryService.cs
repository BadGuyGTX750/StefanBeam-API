using RestApplication.Data;
using RestApplication.Models.Category;

namespace RestApplication.Repositories
{
    public class SubCategoryService
    {
        private readonly SubCategoryRepository repository;


        public SubCategoryService(SubCategoryRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> AddSubCategory(SubCategoryModel category)
        {
            return await repository.AddSubCategory(category);
        }


        public async Task<SubCategoryModel> GetSubCategoryById(Guid id)
        {
            return await repository.GetSubCategoryById(id);
        }


        public async Task<SubCategoryModel> GetSubCategoryByName(string name)
        {
            return await repository.GetSubCategoryByName(name);
        }


        public async Task<bool> DeleteSubCategoryById(Guid id)
        {
            return await repository.DeleteSubCategoryById(id);
        }


        public async Task<bool> DeleteSubCategoryByName(string name)
        {
            return await repository.DeleteSubCategoryByName(name);
        }
    }
}
