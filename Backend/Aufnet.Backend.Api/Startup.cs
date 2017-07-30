using Aufnet.Backend.Data;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Identity;

using System;
using System.Text;
using AspNet.Security.OpenIdConnect.Primitives;
using Aufnet.Backend.ApiServiceShared.Models;
using Aufnet.Backend.ApiServiceShared.Models.Shared;
using Aufnet.Backend.Services;
using Aufnet.Backend.Services.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace Aufnet.Backend.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // Configure the Serilog pipeline
            if (env.EnvironmentName == "Production")
            {
                Log.Logger = new LoggerConfiguration()
                    //.MinimumLevel.Debug()
                    .MinimumLevel.Information()
                    .Enrich
                    .FromLogContext()
                    .WriteTo.RollingFile("logs\\gardenia-api-{Date}.txt")
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    //.MinimumLevel.Debug()
                    .MinimumLevel.Information()
                    .Enrich
                    .FromLogContext()
                    .WriteTo.LiterateConsole()
                    .CreateLogger();
            }
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:AufnetDB"]);
                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Add Identity services to the services container.
            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });
            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {


                //email confirmation
                //options.SignIn.RequireConfirmedEmail = true;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;


                // Cookie settings
                //options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                //options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                //options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                // User settings
                options.User.RequireUniqueEmail = true;
            });


            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();
                // Enable the token endpoint.
                options.EnableTokenEndpoint("/connect/token");

                options.SetAccessTokenLifetime(TimeSpan.FromHours(24));
                // Enable the password flow.
                options.AllowPasswordFlow();
                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });


            //app services
            services.AddScoped<ICustomerService, CustomersService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<ICustomerProfileService, CustomerProfilesService>();
            services.AddScoped<IEmailService, SendGridEmailService>();
            //services.AddScoped<IMessagingService, MessagingService>();
            //services.AddTransient<IDscProcessorService, DscProcessorService>();
            //services.AddTransient<IStaffService, StaffService>();
            //services.AddTransient<ISftpService, SftpService>();
            //services.AddTransient<IMailer, SendGridMailer>();
            //services.AddTransient<EndOfShiftFileWriteJob>();
            //services.AddTransient<DscDownloadJob>();

            //services.AddTransient<IEmailSender, AuthMessageSender>();

            //Configuration service and options
            services.AddSingleton<IConfiguration>(Configuration);

            //options provider

            //cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //configure logging 
            loggerFactory.AddSerilog();

            //db seed
            var dbContext = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            var userManager = app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            DbInitializer.Initialize(dbContext, userManager, roleManager).Wait();

            ////init messaging
            //var msgSvc = app.ApplicationServices.GetRequiredService<IMessagingService>();
            //msgSvc.InitMessaging();


            //exception handler
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null){
                        var ex = error.Error;
                        var responseJson = JsonConvert.SerializeObject(new ErrorDto(ex.Source, ex.Message));
                        await context.Response.WriteAsync(responseJson, Encoding.UTF8);
                    }
                });
            });
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            //cors
            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            //OAuth
            // Register the validation middleware, that is used to decrypt
            // the access tokens and populate the HttpContext.User property.
            app.UseOAuthValidation();
            // Register the OpenIddict middleware.
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));


            //todo: remove test code
            //var dscSvc = app.ApplicationServices.GetRequiredService<IDscProcessorService>();
            //var staffSvc = app.ApplicationServices.GetRequiredService<IStaffService>();
            //DateTimeOffset salesDay = DateTime.UtcNow.ToTradingTimeFromUtc().DayStart();
            //TestDataGenerator.GenerateAndProcessDscFiles(dscSvc, staffSvc, salesDay, salesDay).Wait();
        }
    }
}
