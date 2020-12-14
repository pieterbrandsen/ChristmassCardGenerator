using ChristmassCardGenerator.Services;
using ChristmassCardGenerator.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmassCardGenerator.Models;
using Microsoft.AspNetCore.Routing;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ChristmassCardGenerator
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();


            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add
        ("/Areas/Identity/Pages/Account/Manage/EmailLists/{0}" + Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
("/Areas/Identity/Pages/Account/Manage/Cards/{0}" + Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
("/Areas/Identity/Pages/Account/Manage/{0}" + Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine.ViewExtension);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline..
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
