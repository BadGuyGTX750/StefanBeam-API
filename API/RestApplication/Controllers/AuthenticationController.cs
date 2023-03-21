using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestApplication.Infrastructure;
using RestApplication.Models.AppUser;
using RestApplication.Services;
using System.Net;
using System.Security.Claims;

namespace RestApplication.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AppUserService service;
        private readonly AccesTokenService accesTokenService;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly UserType userType;

        public AuthenticationController(
            AppUserService appUserService,
            AccesTokenService accesTokenService,
            JwtTokenGenerator jwtTokenGenerator,
            IOptions<UserType> userType)
        {
            this.service = appUserService;
            this.accesTokenService = accesTokenService;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userType = userType.Value;
        }

        [HttpPost("/api/auth/register")]
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
                return BadRequest(StatusCodes.Status409Conflict);

            password = BCrypt.Net.BCrypt.HashPassword(password, 12); // remember to *Salt* later
            AppUserModel appUserToAdd = new AppUserModel();
            appUserToAdd.firstName = firstName;
            appUserToAdd.lastName = lastName;
            appUserToAdd.email = email;
            appUserToAdd.password = password;

            if (await service.AddAppUser(appUserToAdd) != true)
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] AppUserRegisterCredentialsModel userCredentials)
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
            Response.Cookies.Append("RestApp-Token", token);

            // give the user a refresh token
            var accesToken = jwtTokenGenerator.GetAccesToken(token);
            Response.Cookies.Append("Refresh-Token", accesToken.refreshToken);

            // add the accesToken in the database
            await accesTokenService.AddToken(accesToken);


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
            Response.Cookies.Append("RestApp-Token", accesToken.accesToken);
            Response.Cookies.Append("Refresh-Token", accesToken.refreshToken);

            return Ok();
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
