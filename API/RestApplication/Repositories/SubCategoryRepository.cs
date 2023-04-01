using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.AppUser;
using RestApplication.Models.Category;

namespace RestApplication.Repositories
{
    public class SubCategoryRepository
    {
        private readonly Entities dbContext;

        public SubCategoryRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddSubCategory(SubCategoryModel subCategory)
        {
            try
            {
                string name = subCategory.name;

                var category = dbContext.subCategories.Where(u => u.name == name).FirstOrDefault();

                string sameName = null;

                if (category != null)
                    sameName = category.name;

                if (sameName != null)
                    return false;

                await dbContext.subCategories.AddAsync(subCategory);
                await dbContext.SaveChangesAsync();
                return true;          
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<SubCategoryModel> GetSubCategoryById(Guid id)
        { 
            try
            {
                return await dbContext.subCategories.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<SubCategoryModel> GetSubCategoryByName(string name)
        {  
            try
            {
                return await dbContext.subCategories.Where(u => u.name == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<SubCategoryModel>> GetSubCategoryByParentName(string parentName)
        {
            try
            {
                return await dbContext.subCategories.Where(u => u.parentCategoryName == parentName).ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> UpdateSubCategory(SubCategoryModel subCategory)
        {
            try
            {
                dbContext.subCategories.Update(subCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteSubCategoryById(Guid id)
        {
            try
            {
                var subCategory = dbContext.subCategories.Where(u => u.id == id).FirstOrDefault();
                dbContext.subCategories.Remove(subCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSubCategoryByName(string name)
        {
            try
            {
                var subCategory = dbContext.subCategories.Where(u => u.name == name).FirstOrDefault();
                dbContext.subCategories.Remove(subCategory);
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
