using DSM.UI.Api.Helpers;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace DSM.UI.Api
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
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<DSMAuthDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DSMAuthServer")), ServiceLifetime.Transient);
            services.AddDbContext<DSMStorageDataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DSMStorageServer")));

            // Configure Settings object
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var cacheDbSettingsSection = this.Configuration.GetSection(nameof(CacheDBSettings));
            services.Configure<CacheDBSettings>(cacheDbSettingsSection);

            var cachingSetings = cacheDbSettingsSection.Get<CacheDBSettings>();

            var client = new MongoClient(cachingSetings.ConnectionString);
            var database = client.GetDatabase(cachingSetings.DatabaseName);
            Helpers.Caching.CacheHelper.CacheDatabase = database;

            // Configure JWT Auth
            var appSettings = appSettingsSection.Get<AppSettings>();

            Helpers.AzureDevOps.RequestHelper.AzureDevOpsToken = appSettings.AzureDevOpsToken;
            Helpers.AzureDevOps.RequestHelper.AzureDevOpsOrganizationName = appSettings.AzureDevOpsOrganizationName;

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       .AddJwtBearer(x =>
       {
           x.Events = new JwtBearerEvents
           {
               OnTokenValidated = context =>
               {
                   var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                   var userName = context.Principal.Identity.Name;
                   var user = userService.GetByUserName(userName);
                   if (user == null || !user.Enabled)
                   {
                       context.Fail("Unauthorized");
                   }
                   return Task.CompletedTask;
               }
           };
           x.RequireHttpsMetadata = false;
           x.SaveToken = true;
           x.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false
           };
           x.IncludeErrorDetails = true;
       });

            services.AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DSM API",
                    Description = "Api for DSM UI",
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!(Ba��na bearer yazmadan sadece tokeni yap��t�r�n.)",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, System.Array.Empty<string>() }
                });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IServerService, ServerService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IWebAccessLogService, WebAccessLogService>();
            services.AddScoped<IAzureDevOpsService, AzureDevOpsService>();
            services.AddScoped<IDatabasePortalService, DatabasePortalService>();
            services.AddScoped<IMonitoringService, MonitoringService>();
            services.AddScoped<IResponsibleService, ResponsibleService>();
            services.AddScoped<ICustomerTrackingService, CustomerTrackingService>();
            services.AddScoped<IInventoryTrackingService, InventoryTrackingService>();
            services.AddScoped<ICustomService, CustomService>();

            services.AddScoped<IDSMOperationLogger, DSMOperationLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Import custom web access log midleware
            app.UseWebAccessLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DSM API V1");
            });

            app.UseRouting();
            
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
