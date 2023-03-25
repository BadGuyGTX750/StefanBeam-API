using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestApplication.Infrastructure;
using RestApplication.Models.AppUser;
using RestApplication.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace RestApplication.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AppUserService service;
        private readonly AccesTokenService accesTokenService;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly EmailSender emailSender;
        private readonly UserType userType;
        private readonly CookieOptions ckOpt;

        public AuthenticationController(
            AppUserService appUserService,
            AccesTokenService accesTokenService,
            JwtTokenGenerator jwtTokenGenerator,
            EmailSender emailSender,
            IOptions<UserType> userType)
        {
            this.service = appUserService;
            this.accesTokenService = accesTokenService;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.emailSender = emailSender;
            this.userType = userType.Value;

            // cookie options
            this.ckOpt = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
        }


        [HttpPost("/api/auth/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AppUserRegisterCredentialsModel userCredentials)
        {
            string firstName = userCredentials.firstName;
            string lastName = userCredentials.lastName;
            string email = userCredentials.email;
            string password = userCredentials.password;

            if (firstName == null || lastName == null || email == null || password == null)
                return BadRequest();

            var appUser = await service.GetAppUserByEmail(email);
            if (appUser != null)
                return StatusCode(409);

            password = BCrypt.Net.BCrypt.HashPassword(password, 12); // remember to *Salt* later
            AppUserModel appUserToAdd = new AppUserModel();
            appUserToAdd.firstName = firstName;
            appUserToAdd.lastName = lastName;
            appUserToAdd.email = email;
            appUserToAdd.password = password;

            if (await service.AddAppUser(appUserToAdd) != true)
            {
                return StatusCode(500);
            }

            // TODO: Send email here
            try
            {
                await emailSender.SendVerificationEmail(appUserToAdd.verificationToken, appUserToAdd.email);
            }
            catch(Exception ex)
            {
                await service.DeleteAppUserByEmail(email);
                return BadRequest();
            }
            

            return Ok();
        }

        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] AppUserLoginCredentialsModel userCredentials)
        {
            string email = userCredentials.email;
            string password = userCredentials.password;

            // check if the request body contains email and password
            if (email == null)
                return BadRequest();
            if (password == null)
                return BadRequest();

            // check if there is a user that exists with the given email
            var appUser = await service.GetAppUserByEmail(email);
            if (appUser == null)
                return NotFound();

            // check if user is verified
            // (he should check it's email and click on the endpoint provided to him)
            if (!appUser.isVerified)
                return Unauthorized();

            // check if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(password, appUser.password))
                return Unauthorized();

            // create claims for the logged in user
            ClaimsIdentity claimsIdentity = this.CreateClaimsIdentity(appUser);

            // delete the acces token in the database that the user may have from the past
            var cookies = Request.Cookies.ToList();
            foreach (var cookie in cookies)
            {
                try
                {
                    if (cookie.Key == "Refresh-Token")
                    {
                        await accesTokenService.DeleteTokenByRefreshToken(cookie.Value);
                    }
                } catch(Exception ex)
                {
                    continue;
                }
            }

            // give the user a token based on his Identity
            string token = jwtTokenGenerator.GenerateToken(claimsIdentity);
            Response.Cookies.Append("RestApp-Token", token, ckOpt);

            // give the user a refresh token
            var accesToken = jwtTokenGenerator.GetAccesToken(token);
            Response.Cookies.Append("Refresh-Token", accesToken.refreshToken, ckOpt);

            // add the accesToken in the database
            await accesTokenService.AddToken(accesToken);


            return Ok();
        }


        [HttpGet("/api/auth/verifyUser")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUser([FromQuery]string token)
        {
            if (token == null)
                return BadRequest();

            var appUser = await service.GetAppUserByVfToken(token);
            if (appUser == null) 
                return NotFound();

            appUser.tokenVerifiedAt = DateTime.UtcNow;

            var difference = appUser.tokenVerifiedAt.Subtract(appUser.verificationTokenCreationDate).TotalHours;

            if (difference > 0.25)
            {
                await service.DeleteAppUserByEmail(appUser.email);
                return BadRequest();
            }

            // Change the isVerified status and issue another token
            // to invalidate the endpoint he was given on email (that token shall be used only once)
            appUser.isVerified = true;
            appUser.verificationToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            

            if (await service.UpdateAppUser(appUser) != true)
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }


        [HttpPost("/api/auth/resetPasswordRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] AppUserLoginCredentialsModel userCredentials)
        {
            // The email provided in the body should be taken from the cookies in the frontend,
            // if the user is logged in
            // (to prevent the user from using a different email than the one he created the account with)
            string email = userCredentials.email;
            string password = userCredentials.password;

            // check if the request body contains email and password
            if (email == null)
                return BadRequest();
            if (password == null)
                return BadRequest();

            // check if there is a user that exists with the given email
            var appUser = await service.GetAppUserByEmail(email);
            if (appUser == null)
                return NotFound();

            // check if the user is verified
            if (!appUser.isVerified)
                return Unauthorized();

            // might need this to restore the user to the initial state if something went wrong
            var copyAppUser = appUser;

            // check if user is verified
            // (he should check it's email and click on the endpoint provided to him)
            if (!appUser.isVerified)
                return Unauthorized();

            appUser.changePasswordRequestDate = DateTime.UtcNow;
            appUser.changedPassword = BCrypt.Net.BCrypt.HashPassword(password, 12); // remember to *Salt* later

            if (await service.UpdateAppUser(appUser) != true)
            {
                return StatusCode(500);
            }

            try
            {
                await emailSender.SendResetPasswordEmail(appUser.verificationToken, appUser.email);
            }
            catch(Exception ex)
            {
                await service.UpdateAppUser(copyAppUser);
                return BadRequest();
            }

            return Ok();
        }


        [HttpGet("/api/auth/resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromQuery] string token)
        {
            if (token == null)
                return BadRequest();

            var appUser = await service.GetAppUserByVfToken(token);
            if (appUser == null)
                return NotFound();

            appUser.newPasswordCreationDate = DateTime.UtcNow;

            var difference = appUser.newPasswordCreationDate.Subtract(appUser.changePasswordRequestDate).TotalHours;

            if (difference > 0.25)
            {
                return BadRequest();
            }

            appUser.password = appUser.changedPassword;
            appUser.changePaswwordToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            if (await service.UpdateAppUser(appUser) != true)
            {
                return StatusCode(500);
            }

            return Ok();
        }




        [HttpPost("/api/auth/refresh")]
        public async Task<IActionResult> Refresh()
        {
            var cookies = Request.Cookies.ToList();
            string? refreshToken = null;
            string? jwtToken = null;

            foreach (var cookie in cookies)
            {
                try
                {
                    if (cookie.Key == "Refresh-Token")
                    {
                        refreshToken = cookie.Value;
                    }
                    if (cookie.Key == "RestApp-Token")
                    {
                        jwtToken = cookie.Value;
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            // check if cookies were found
            if (refreshToken == null || jwtToken == null)
                return Unauthorized();

            // validate the jwt accesToken
            var accesToken = await accesTokenService.GetTokenByRefreshToken(refreshToken);
            if (!jwtTokenGenerator.ValidateToken(accesToken.accesToken))
                return Unauthorized();

            // update the token with a new acces token and a new refresh token and a new refrshToken expiration date
            accesToken = jwtTokenGenerator.UpdateAccesToken(accesToken);

            // update the acces token in the database
            await accesTokenService.UpdateToken(accesToken);

            // append new cookies
            Response.Cookies.Append("RestApp-Token", accesToken.accesToken, ckOpt);
            Response.Cookies.Append("Refresh-Token", accesToken.refreshToken, ckOpt);

            return Ok();
        }


        [HttpGet("/api/auth/getEmailFromCookie")]
        public async Task<IActionResult> GetEmailFromCookie()
        {
            var cookies = Request.Cookies.ToList();
            string? jwtToken = null;

            foreach (var cookie in cookies)
            {
                try
                {
                    if (cookie.Key == "RestApp-Token")
                    {
                        jwtToken = cookie.Value;
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            List<Claim> claims = token.Claims.ToList();

            foreach (var claim in claims)
            {
                if (claim.Type == "Email")
                    return Ok(claim.Value);
            }
            return NotFound();
        }




        // These endpoints have testing purposes; Admin should be able to acces both, visitor just his;

        [HttpGet("/api/auth/test/getsecret/basicuser")]
        [Authorize(Policy = "Basic")]
        public IActionResult VisitorGetSecret()
        {
            return Ok();
        }


        [HttpGet("/api/auth/test/getsecret/admin")]
        [Authorize(Policy = "Admin")]
        public IActionResult AdminGetSecret()
        {
            return Ok();
        }


        private ClaimsIdentity CreateClaimsIdentity(AppUserModel appUser)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Email", appUser.email),
                new Claim("Role", appUser.role)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            return claimsIdentity;
        }


    }
}
