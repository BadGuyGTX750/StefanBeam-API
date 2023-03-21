using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Category;
using System.Linq;

namespace RestApplication.Repositories
{
    public class MiddleCategoryRepository
    {
        private readonly Entities dbContext;

        public MiddleCategoryRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddMiddleCategory(MiddleCategoryModel middleCategory)
        {
            try
            {
                string name = middleCategory.name;

                var category = dbContext.middleCategories.Where(u => u.name == name).FirstOrDefault();

                string sameName = null;

                if (category != null)
                    sameName = category.name;

                if (sameName != null)
                    return false;

                dbContext.middleCategories.Add(middleCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<MiddleCategoryModel> GetMiddleCategoryById(Guid id)
        {
            try
            {
                return await dbContext.middleCategories.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<MiddleCategoryModel> GetMiddleCategoryByName(string name)
        {
            try
            {
                return await dbContext.middleCategories.Where(u => u.name == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<MiddleCategoryModel>> GetMiddleCategoriesByTopCategoryName(string name)
        {
            try
            {
                return await dbContext.middleCategories.Where(u => u.parentCategoryName == name).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> AssignToTopCategory(SubCategoryModel middleCategory, MiddleCategoryModel topCategory)
        {
            try
            {
                Guid parentId = topCategory.id;
                middleCategory.parentCategoryId = parentId;
                dbContext.subCategories.Update(middleCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteMiddleCategoryById(Guid id)
        {
            try
            {
                var middleCategory = dbContext.middleCategories.Where(u => u.id == id).FirstOrDefault();
                dbContext.middleCategories.Remove(middleCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteMiddleCategoryByName(string name)
        {
            try
            {
                var middleCategory = dbContext.middleCategories.Where(u => u.name == name).FirstOrDefault();
                dbContext.middleCategories.Remove(middleCategory);
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
