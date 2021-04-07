using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Services;
using Integracja.Server.Web.Ulitities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Integracja.Server.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LocalDbConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<ProfilePicturesClaimsPrincipalFactory>();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
           {
               options.LoginPath = "/Identity/Account/Login";
               options.AccessDeniedPath = "/Identity/Account/Login";
           });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });

            services.AddAutoMapper(new[] {
                typeof(ApplicationDbContext),
                typeof(Controllers.ApplicationController) });
            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "GryArea",
                    areaName: "Gry",
                    pattern: "Gry/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "TrybyGryArea",
                    areaName: "TrybyGry",
                    pattern: "TrybyGry/{controller=GamemodeSelect}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "KategorieArea",
                    areaName: "Kategorie",
                    pattern: "Kategorie/{controller=CategoryForQuestion}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "PytaniaArea",
                    areaName: "Pytania",
                    pattern: "Pytania/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "PanelAdminaArea",
                    areaName: "PanelAdmina",
                    pattern: "PanelAdmina/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "KontoArea",
                    areaName: "Konto",
                    pattern: "Konto/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "HistoriaArea",
                    areaName: "Historia",
                    pattern: "Historia/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "Gry",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
