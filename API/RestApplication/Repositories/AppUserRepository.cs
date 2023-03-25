using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.AppUser;
using RestApplication.Models.Product;

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


        public async Task<AppUserModel> GetAppUserByCPToken(string token)
        {
            try
            {
                return await dbContext.appUsers.Where(u => u.changePaswwordToken == token).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<AppUserModel> GetAppUserByVfToken(string token)
        {
            try
            {
                return await dbContext.appUsers.Where(u => u.verificationToken == token).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> UpdateAppUser(AppUserModel appUser)
        {
            try
            {
                dbContext.appUsers.Update(appUser);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteAppUserByEmail(string email)
        {
            try
            {
                var appUser = await dbContext.appUsers.Where(u => u.email == email).FirstAsync();
                dbContext.appUsers.Remove(appUser);
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
