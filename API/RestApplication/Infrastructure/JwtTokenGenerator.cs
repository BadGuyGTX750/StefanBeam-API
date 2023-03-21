using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestApplication.Models.AppUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApplication.Infrastructure
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings jwtSettings;


        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }


        public string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret));

            //  this way of constucting an object is called an object initializer;
            //  this only works with attributes that have public getter and setter,
            // or attributes which are public
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.TokenExpirationMinutes),
                Issuer= jwtSettings.Issuer,
                Audience= jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // Generate a new acces token which must be added in the database (For login purposes)
        public AccesToken GetAccesToken(string jwtToken)
        {
            var token = new AccesToken();
            token.accesToken = jwtToken;
            token.refreshToken = Guid.NewGuid().ToString();
            token.expirationDate = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays);

            return token;           
        }


        // Validate the old JwtToken for all, except the expiration time
        public bool ValidateToken(string token)
        {
            // Not yet implemented
            return true;
        }


        // Get a new accesToken if you can provide the old accesToken from the cookies
        public AccesToken UpdateAccesToken(AccesToken accesToken)
        {
            accesToken.refreshToken = Guid.NewGuid().ToString();
            // generate a new JwtToken based on the claims extracted from the current token
            var oldJwtToken = accesToken.accesToken;
            accesToken.accesToken = this.GenerateToken(this.GetClaimsIdentity(oldJwtToken));
            accesToken.expirationDate = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays);

            return accesToken;
        }

        // Create a new pair of ClaimsIdentity extracted from the oldJwtToken
        private ClaimsIdentity GetClaimsIdentity(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var claims = token.Claims;
            
            List<Claim> claimsToAdd= new List<Claim>();

            foreach (var claim in claims )
            {
                if (claim.Type == "Email")
                    claimsToAdd.Add(new Claim(claim.Type, claim.Value));
                if (claim.Type == "Role")
                    claimsToAdd.Add(new Claim(claim.Type, claim.Value));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsToAdd);

            return claimsIdentity;
        }
    }
}
