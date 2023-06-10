using Dinewell.Application.Logging;
using Dinewell.Application.UseCases.Commands;
using Dinewell.DataAccess;
using Dinewell.Implementation.UseCases.Commands;
using Dinewell.Implementation.Validators;
using Dinewell.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Implementation.UseCases.Queries;
using Dinewell.Application.UseCaseHandling;
using Dinewell.API.ErrorLogging;
using Microsoft.EntityFrameworkCore;
using Dinewell.API.DTO;
using Dinewell.API.JWT.TokenStorage;
using Dinewell.API.JWT;
using Dinewell.API.Extensions;
using Dinewell.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Dinewell.API.Middleware;
using Dinewell.Implementation.Logging;
using System.IO;
using System.Reflection;
using Dinewell.Application.Emails;
using Dinewell.Implementation.Emails;

namespace Dinewell.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DinewellContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer("Data Source=localhost; Initial Catalog = Dinewell; Integrated Security = true").UseLazyLoadingProxies();
                return new DinewellContext(builder.Options);
            });
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<DinewellContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            services.AddTransient<IErrorLogger,ConsoleErrorLogger>();
            services.AddTransient<ICreateRestaurantCommand, EfCreateRestaurantCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<ICreateSidesCommand, EfCreateSidesCommand>();
            services.AddTransient<ICreateFoodCategoryCommand, EfCreateFoodCategoryCommand>();
            services.AddTransient<ICreateRestaurantMenuCommand, EfCreateRestaurantMenuCommand>();
            services.AddTransient<ISearchUsersQuery, EfSearchUsersQuery>();
            services.AddTransient<ISearchSpecificUsersQuery, EfSearchSpecificUsersQuery>();
            services.AddTransient<ISearchRestaurantSidesQuery, EfSearchRestaurantSidesQuery>();
            services.AddTransient<ISearchSpecificRestaurantSidesQuery, EfSearchSpecificRestaurantSidesQuery>();
            services.AddTransient<ICreateRestaurantFoodCategoryCommand, EfCreateRestaurantFoodCategoryCommand>();
            services.AddTransient<ISearchOrdersQuery, EfSearchOrdersQuery>();
            services.AddTransient<ISearchSpecificOrdersQuery, EfSearchSpecificOrdersQuery>();
            services.AddTransient<ICreateRestaurantSideCommand, EfCreateRestaurantSideCommand>();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IUpdateRestaurantCommand, EfUpdateRestaurantCommand>();
            services.AddTransient<IUpdateFoodCategoryCommand, EfUpdateFoodCategoryCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IUpdateSideCommand, EfUpdateSideCommand>();
            services.AddTransient<IUpdateRestaurantMenuCommand, EfUpdateRestaurantMenuCommand>();
            services.AddTransient<IUpdateRestautantSideCommand, EfUpdateRestautantSideCommand>();
            services.AddTransient<ISearchSidesQuery, EfSearchSidesQuery>();
            services.AddTransient<ISearchSpecificSideQuery, EfSearchSpecificSideQuery>();
            services.AddTransient<ISearchFoodCategoriesQuery, EfUseCase>();
            services.AddTransient<ISearchMealsQuery, EfSearchMealsQuery>();
            services.AddTransient<ISearchSpecificMealQuery, EfSearchSpecificMealQuery>();
            services.AddTransient<ISearchRestaurantsQuery, EfSearchRestaurantsQuery>();
            services.AddTransient<ISearchSpecificRestaurantsQuery, EfSearchSpecificRestaurantsQuery>();
            services.AddTransient<ISearchSpecificFoodCategoryQuery, EfSearchSpecificFoodCategoryQuery>();
            services.AddTransient<IDeleteRestaurantCommand, EfDeleteRestaurantCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IDeleteRestaurantMealCommand, EfDeleteRestaurantMealCommand>();
            services.AddTransient<IDeleteRestaurantFoodCategoryCommand, EfDeleteRestaurantFoodCategoryCommand>();
            services.AddTransient<ISearchFoodsQuery, EfSearchFoodsQuery>();
            services.AddTransient<ISearchSpecificFoodsQuery, EfSearchSpecificFoodsQuery>();
            services.AddTransient<ICreateFoodCommand, EfCreateFoodCommand>();
            services.AddTransient<IUpdateFoodCommand, EfUpdateFoodCommand>();
            services.AddTransient<IDeleteRestaurantSideCommand, EfDeleteRestaurantSideCommand>();

            services.AddTransient<IUserUpdateUserCommand, EfUserUpdateUserCommand>();
            services.AddTransient<IUserSearchHisOrdersQuery, EfUserSearchHisOrdersQuery>();
            services.AddTransient<IUserSearchRestaurantsQuery, EfUserSearchRestaurantsQuery>();
            services.AddTransient<IUserSearchSpecificRestaurantsQuery, EfUserSearchSpecificRestaurantsQuery>();
            services.AddTransient<IUserSearchFoodCategoriesQuery, EfUserSearchFoodCategoriesQuery>();
            services.AddTransient<IUserSearchMealsQuery, EfUserSearchMealsQuery>();
            services.AddTransient<IUserSearchSpecificFoodCategoryQuery, EfUserSearchSpecificFoodCategoryQuery>();
            services.AddTransient<IUserSearchSpecificMealQuery, EfUserSearchSpecificMealQuery>();

            services.AddTransient<IEmailSender>(x =>
            new SmtpEmailSender(appSettings.EmailOptions.FromEmail,
                                appSettings.EmailOptions.Password,
                                appSettings.EmailOptions.Port,
                                appSettings.EmailOptions.Host));

            services.AddTransient<QueryHandler>();
            services.AddTransient<CommandHandler>();
            services.AddValidators();
            services.AddHttpContextAccessor();
            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    return new UnauthorizedActor();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;

                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JWTActor
                {
                    Email = email,
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                };
            });

            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
            services.AddTransient<ICommandHandler, CommandHandler>();

            services.AddControllers();

            services.AddJwt(appSettings);

            services.AddTransient<IQueryHandler>(x =>
            {
                var actor = x.GetService<IApplicationActor>();
                var logger = x.GetService<IUseCaseLogger>();
                var queryHandler = new QueryHandler();
                var timeTrackingHandler = new TimeTrackingQueryHandler(queryHandler);
                var loggingHandler = new LoggingQueryHandler(timeTrackingHandler, actor, logger);
                var decoration = new AuthorizationQueryHandler(actor, loggingHandler);

                return decoration;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dinewell.API", Version = "v1" });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dinewell.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
