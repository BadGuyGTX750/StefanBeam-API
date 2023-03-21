using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Category;

namespace RestApplication.Repositories
{
    public class TopCategoryRepository
    {
        private readonly Entities dbContext;

        public TopCategoryRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddTopCategory(TopCategoryModel topCategory)
        {
            try
            {
                string name = topCategory.name;

                var category = dbContext.topCategories.Where(u => u.name == name).FirstOrDefault();

                string sameName = null;

                if (category != null)
                    sameName = category.name;

                if (sameName != null)
                    return false;

                await dbContext.topCategories.AddAsync(topCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<TopCategoryModel> GetTopCategoryById(Guid id)
        {
            try
            {
                return await dbContext.topCategories.Where(u => u.id == id).FirstAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<TopCategoryModel> GetTopCategoryByName(string name)
        {
            try
            {
                return await dbContext.topCategories.Where(u => u.name == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<TopCategoryModel>> GetAll()
        {   
            try
            {
                return await dbContext.topCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> DeleteTopCategoryById(Guid id)
        {
            try
            {
                var topCategory = dbContext.topCategories.Where(u => u.id == id).FirstOrDefault();
                dbContext.topCategories.Remove(topCategory);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteTopCategoryByName(string name)
        {
            try
            {
                var topCategory = dbContext.topCategories.Where(u => u.name == name).FirstOrDefault();
                dbContext.topCategories.Remove(topCategory);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
