using System;
using System.Threading.Tasks;
using FashFuns.Common;
using FashFuns.Common.Exceptions.GlobalHandler;
using FashFuns.Common.Exceptions.GlobalHandler.Contracts;
using FashFuns.Common.IdentityConfiguration;
using FashFuns.Common.ResponseBuilder;
using FashFuns.Common.ResponseBuilder.Contracts;
using FashFuns.Database;
using FashFuns.Domain.DataSourceAdapters.SQL;
using FashFuns.Domain.DataSourceAdapters.SQL.Contracts;
using FashFuns.Domain.DbConnectionFactory;
using FashFuns.Domain.Repositories.Identity;
using FashFuns.Domain.Repositories.Identity.Contracts;
using FashFuns.Domain.Repositories.Products;
using FashFuns.Domain.Repositories.Products.Contracts;
using FashFuns.Services.IdentityServices;
using FashFuns.Services.IdentityServices.Contracts;
using FashFuns.Services.ProductServices;
using FashFuns.Services.ProductServices.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace FashFuns.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ConfigurationManager.SetAppSettingsProperties(Configuration);

            _environment = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurateDbConext(services);

            services.AddCors();

            ConfigureJwtAuthService(services);

            services.AddMvc(options => {
                options.CacheProfiles.Add(CacheControlProfiles.Default,
                    new CacheProfile()
                    {
                        Duration = 60
                    });
                options.CacheProfiles.Add(CacheControlProfiles.TwoHours,
                    new CacheProfile()
                    {
                        Duration = 7200
                    });
                options.CacheProfiles.Add(CacheControlProfiles.HalfDay,
                    new CacheProfile()
                    {
                        Duration = 43200
                    });
            }).AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            }).AddDataAnnotationsLocalization();

            services.AddScoped<IGlobalExceptionHandler, GlobalExceptionHandler>();
            services.AddScoped<IGlobalExceptionFactory, GlobalExceptionFactory>();

            services.AddTransient<IResponseFactory, ResponseFactory>();
            services.AddTransient<IIdentityRepository, IdentityRepository>();

            services.AddTransient<IIdentityRepositoriesFactory, IdentityRepositoriesFactory>();

            services.AddTransient<IUserIdentityService, UserIdentityService>();
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<ISqlDbContext, SqlDbContext>();
            services.AddTransient<ISqlContextFactory, SqlContextFactory>();

            services.AddTransient<IProductsRepositoriesFactory, ProductsRepositoriesFactory>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();

            services.Add(new ServiceDescriptor(typeof(ISqlDbContext),
                t => new SqlDbContext(new FashFunsDbContext(t.GetService<DbContextOptions<FashFunsDbContext>>())), ServiceLifetime.Transient)
            );
        }

        private void ConfigurateDbConext(IServiceCollection services)
        {
            services.AddDbContext<FashFunsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringNames.Local)));
            services.AddScoped(p => new FashFunsDbContext(p.GetService<DbContextOptions<FashFunsDbContext>>()));
        }

        private void ConfigureJwtAuthService(IServiceCollection services)
        {
            var signingKey = AuthOptions.GetSymmetricSecurityKey(ConfigurationManager.AppSettings.TokenSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,

                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE_LOCAL,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context => {
                        var accessToken = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public void Configure(IApplicationBuilder app, FashFunsDbContext ctx, IHostingEnvironment env, IGlobalExceptionFactory globalExceptionFactory)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseExceptionHandler(builder => {
                builder.Run(
                        async context => {
                            IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                            IGlobalExceptionHandler globalExceptionHandler = globalExceptionFactory.New();

                            await globalExceptionHandler.HandleException(context, error, _environment.IsDevelopment());
                        });
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
