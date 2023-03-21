using Microsoft.EntityFrameworkCore;
using RestApplication.Models.AppUser;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class AccesTokenService
    {
        private readonly AccesTokenRepository repository;


        public AccesTokenService(AccesTokenRepository repository)
        { 
            this.repository = repository;
        }


        public async Task<bool> AddToken(AccesToken token)
        {
            return await repository.AddToken(token);
        }


        public async Task<bool> UpdateToken(AccesToken token)
        {
            return await repository.UpdateToken(token);
        }


        public async Task<AccesToken> GetTokenByRefreshToken(string refreshToken)
        {
            return await repository.GetTokenByRefreshToken(refreshToken);
        }


        public async Task<bool> DeleteTokenByRefreshToken(string refreshToken)
        {
            return await repository.DeleteTokenByRefreshToken(refreshToken);
        }
    }
}
