using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TheaterApplication.Bll.Helpers;
using TheaterApplication.Bll.Helpers.Interfaces;
using TheaterApplication.Bll.Services;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Bll.Storages;
using TheaterApplication.Bll.Storages.Interfaces;
using TheaterApplication.Dal;
using TheaterApplication.Dal.Builders;
using TheaterApplication.Dal.Builders.Interfaces;
using TheaterApplication.Dal.Repositories;
using TheaterApplication.Dal.Repositories.Interfaces;
using TheaterApplication.Utils.Settings;
using TheaterApplication.WebApi.ExceptionHandling;
using TheaterApplication.WebApi.Mapping;

namespace TheaterApplication.WebApi
{
    public class Startup
    {
        readonly string AllowLocalSpecificOrigins = "_allowLocalSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowLocalSpecificOrigins, builder =>
                {
                    builder.
                        SetIsOriginAllowed(x => new Uri(x).Host == "localhost").
                        AllowAnyMethod().
                        AllowAnyHeader();
                });
            });

            var mvcBuilder = services.AddControllers();

            SetupNewtonsoft(mvcBuilder);

            var appSettings = new ApplicationSettings();

            Configuration.Bind(appSettings);

            services.AddSingleton(appSettings);
            services.AddSingleton(appSettings.Token);

            

            string connectionString = Configuration.
                GetConnectionString("TheaterConnectionString");

            services.AddDbContext<TheaterDbContext>(options => 
                options.UseNpgsql(connectionString).
                    UseSnakeCaseNamingConvention()
            );

            //mapping
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            AddRepositories(services);
            AddServices(services);

            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();

            services.AddScoped<IApplicationStorage, ApplicationStorage>();

            services.AddTransient<IParametersBuilder, NpgsqlParametersBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandling();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowLocalSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            services.AddScoped<IPerformanceScheduleRepository, PerformanceScheduleRepository>();
            services.AddScoped<IPerformancePosterRepository, PerformancePosterRepository>();
            services.AddScoped<IPerformanceBookingRepository, PerformanceBookingRepository>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPerformanceService, PerformanceService>();
            services.AddScoped<IPerformanceScheduleService, PerformanceScheduleService>();
            services.AddScoped<IPerformancePosterService, PerformancePosterService>();
            services.AddScoped<IPerformanceBookingService, PerformanceBookingService>();
        }

        private void SetupNewtonsoft(IMvcBuilder mvcBuilder)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            mvcBuilder.AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = contractResolver;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //Setup global newtonsoft
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver
            };
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TheaterDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
