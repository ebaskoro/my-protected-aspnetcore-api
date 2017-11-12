using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Data;


namespace Api
{

    /// <summary>
    /// Application startup.
    /// </summary>
    public class Startup
    {

        private readonly IConfiguration _configuration;


        /// <summary>
        /// Creates an application startup.
        /// </summary>
        /// <param name="configuration">Configuration to use.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseOptions = new DatabaseOptions();
            _configuration
                .GetSection("Database")
                .Bind(databaseOptions);
            var connectionString = databaseOptions.ConnectionString;
            var databaseProvider = databaseOptions.Provider;

            if (databaseProvider.Equals("MySql", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDbContext<HeroContext>(options => options.UseMySql(connectionString));
            }
            else
            {
                services.AddDbContext<HeroContext>(options => options.UseInMemoryDatabase(connectionString));
            }
            
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddCors(options =>
                options.AddPolicy("AllowAll", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

            services
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            var identityServerOptions = new IdentityServerOptions();
            _configuration
                .GetSection("IdentityServer")
                .Bind(identityServerOptions);
            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = identityServerOptions.Authority;
                    options.RequireHttpsMetadata = identityServerOptions.RequireHttpsMetadata;
                    options.ApiName = identityServerOptions.ApiName;
                });
        }

        
        /// <summary>
        /// Configures the middleware used by the application.
        /// </summary>
        /// <param name="app">Application to configure.</param>
        /// <param name="heroContext">Hero context to use.</param>
        public void Configure(IApplicationBuilder app, HeroContext heroContext)
        {
            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseMvc();

            heroContext.Database.EnsureCreated();
        }

    }

}
