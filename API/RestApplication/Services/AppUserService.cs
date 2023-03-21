using RestApplication.Models.AppUser;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class AppUserService
    {
        private readonly AppUserRepository appUserRepository;

        public AppUserService(AppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }


        public async Task<AppUserModel> GetAppUserByEmail(string email)
        {
            var appUser = await appUserRepository.GetAppUserByEmail(email);
            return appUser;
        }


        public async Task<bool> AddAppUser(AppUserModel appUser)
        {
            return await appUserRepository.AddAppUser(appUser);
        }
    }
}
