using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RestApplication.Infrastructure;
using RestApplication.Models.AppUser;
using RestApplication.Repositories;
using RestApplication.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using RestApplication.Data;
using Microsoft.AspNetCore.Authorization;
using RestApplication.Controllers;
using System.Net.Http;
using Microsoft.OpenApi.Models;

namespace RestApplication.DependencyInjection
{
    public static class DependencyInjection
    {
        // *this* keyword before first parameter of the method
        // indicates that this is an *extension method*
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            // Add services
            services.AddScoped<AppUserService>();
            services.AddScoped<TopCategoryService>();
            services.AddScoped<MiddleCategoryService>();
            services.AddScoped<SubCategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<WeightPriceService>();
            services.AddScoped<FlavorQuantityService>();
            services.AddScoped<PhotoAttachmentService>();
            services.AddScoped<AccesTokenService>();

            // Add repositories
            services.AddScoped<AppUserRepository>();
            services.AddScoped<TopCategoryRepository>();
            services.AddScoped<MiddleCategoryRepository>();
            services.AddScoped<SubCategoryRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<WeightPriceRepository>();
            services.AddScoped<FlavorQuantityRepository>();
            services.AddScoped<PhotoAttachmentRepository>();
            services.AddScoped<AccesTokenRepository>();

            // Add controllers
            services.AddControllers();

            // Add other services
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<EmailSender>();

            // Configuration objects
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<UserTypes>(configuration.GetSection("UserTypes"));
            services.Configure<ProjectPaths>(configuration.GetSection("ProjectPaths"));

            // Configuration secrets
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("./secret.json", optional: false, reloadOnChange: true);
            var secretConfig = configBuilder.Build();

            services.Configure<EmailCredentials>(secretConfig.GetSection("EmailCredentials"));

            // Add Authentication and Authorization
            services.AddAuth(configuration);

            // Configure Entity Framework
            services.AddDbContext<Entities>(options =>
            {
                options.UseSqlServer(secretConfig.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StefanBeam API", Version = "v1.0.0"});
            });

            return services;
        }






        // Configure Authentication and Authorization Services to use JwtBearer
        // Cookie which is validated by the TokenValidationParameters is taken from the OnMessageReceived Event by it's name
        private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetSection("JwtSettings:Issuer").Value,
                        ValidAudience = configuration.GetSection("JwtSettings:Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(configuration.GetSection("JwtSettings:Secret").Value)),

                        // For some reason, the token was valid for additional time after it expired, the line below solved it
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["RestApp-Token"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(o =>
            {
                var userTypes = configuration.Get<UserTypes>().userTypes;

                // Add policies of authorization for each UserType that is defined in appsettings.json

                foreach (UserType userType in userTypes)
                {
                    o.AddPolicy(userType.Role, p =>
                    {
                        p.RequireClaim("Email");
                        p.RequireClaim("Role", userType.Role);
                    });
                }
            });

            return services;
        }
    }
}
