using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NSwag;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Implementations;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Utils.Helpers;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI
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
            services.AddSingleton(factory => new ApplicationProperties
            {
                DatabaseConnectionString = "Data Source=localhost,1433;User ID=sa;Password=<2019!Pass>;Database=master"
            });

            // Stores
            services.AddScoped<IUserAuthenticationStore, UserAuthenticationStore>();

            // Services
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<UserCookieAuthenticationEvents>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = ".CarWashService.Cookies.Session";
                options.EventsType = typeof(UserCookieAuthenticationEvents);
            }).AddGoogle(options =>
            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                options.ClaimActions.Clear();
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
            }).AddVkontakte(options =>
            {
                options.ClientId = Configuration["Authentication:VK:ClientId"];
                options.ClientSecret = Configuration["Authentication:VK:ClientSecret"];
                options.ClaimActions.Clear();
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
            });

            services.AddControllersWithViews();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Car Wash Service API";
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Map(new PathString("/api"), builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    var msg = JsonConvert.SerializeObject(new { Message = "Invalid request path" });
                    await context.Response.WriteAsync(msg).ConfigureAwait(false);
                });
            });

            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }
    }
}