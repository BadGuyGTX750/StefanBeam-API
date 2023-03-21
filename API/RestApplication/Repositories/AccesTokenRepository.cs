using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.AppUser;

namespace RestApplication.Repositories
{
    public class AccesTokenRepository
    {
        private readonly Entities dbContext;


        public AccesTokenRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddToken(AccesToken token)
        {
            try
            {
                await dbContext.accesTokens.AddAsync(token);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UpdateToken(AccesToken token)
        {
            try
            {
                dbContext.accesTokens.Update(token);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<AccesToken> GetTokenByRefreshToken(string refreshToken)
        {
            try
            {
                return await dbContext.accesTokens.Where(u => u.refreshToken == refreshToken).FirstAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> DeleteTokenByRefreshToken(string refreshToken)
        {
            try
            {
                var token = dbContext.accesTokens.Where(u => u.refreshToken == refreshToken).FirstOrDefault();
                dbContext.accesTokens.Remove(token);
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
