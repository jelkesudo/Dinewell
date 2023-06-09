using Dinewell.API.DTO;
using Dinewell.API.ErrorLogging;
using Dinewell.API.JWT;
using Dinewell.Application.Logging;
using Dinewell.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddLogger(this IServiceCollection services)
        {
            services.AddTransient<IErrorLogger>(x =>
            {
                var accesor = x.GetService<IHttpContextAccessor>();

                if (accesor == null || accesor.HttpContext == null)
                {
                    return new ConsoleErrorLogger();
                }

                var logger = accesor.HttpContext.Request.Headers["Logger"].FirstOrDefault();


                return new ConsoleErrorLogger();
            });
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<CreateRestaurantValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<CreateFoodCategoryValidator>();
            services.AddTransient<CreateRestaurantMenuValidator>();
            services.AddTransient<CreateSidesValidator>();
            services.AddTransient<CreateRestaurantSidesValidator>();
            services.AddTransient<CreateRestaurantFoodCategoriesValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<CreateFoodValidator>();

            services.AddTransient<UpdateRestaurantValidator>();
            services.AddTransient<UpdateFoodCategoryValidator>();
            services.AddTransient<UpdateSideValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<UpdateRestaurantMealValidator>();
            services.AddTransient<UpdateRestaurantSideValidator>();
            services.AddTransient<UpdateFoodValidator>();
        }

        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Jwt.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if (!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
