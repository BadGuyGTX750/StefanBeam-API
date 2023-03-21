using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.AppUser;

namespace RestApplication.Repositories
{
    public class AppUserRepository
    {
        private readonly Entities dbContext;


        public AppUserRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddAppUser(AppUserModel appUser)
        {
            try
            {
                await dbContext.appUsers.AddAsync(appUser);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<AppUserModel> GetAppUserByEmail(string email)
        {
            try
            {
                return await dbContext.appUsers.Where(u => u.email == email).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
