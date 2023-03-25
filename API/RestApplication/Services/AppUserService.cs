using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestApplication.Models.AppUser;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class AppUserService
    {
        private readonly AppUserRepository repository;


        public AppUserService(AppUserRepository appUserRepository)
        {
            this.repository = appUserRepository;
        }


        


        public async Task<bool> AddAppUser(AppUserModel appUser)
        {
            return await repository.AddAppUser(appUser);
        }


        public async Task<AppUserModel> GetAppUserByEmail(string email)
        {
            var appUser = await repository.GetAppUserByEmail(email);
            return appUser;
        }


        public async Task<AppUserModel> GetAppUserByVfToken(string token)
        {
            var appUser = await repository.GetAppUserByVfToken(token);
            return appUser;
        }


        public async Task<AppUserModel> GetAppUserByCPToken(string token)
        {
            var appUser = await repository.GetAppUserByCPToken(token);
            return appUser;
        }


        public async Task<bool> UpdateAppUser(AppUserModel appUser)
        {
            return await repository.UpdateAppUser(appUser);
        }


        public async Task<bool> DeleteAppUserByEmail(string email)
        {
            return await repository.DeleteAppUserByEmail(email);
        }
    }
}
