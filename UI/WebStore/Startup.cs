using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Controllers;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Interfaces.Api;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.Products;
using WebStore.Clients.Orders;
using WebStore.Interfaces.Services;
using WebStore.Clients.Identity;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {       
            services.AddIdentity<User, Role>()
               //.AddEntityFrameworkStores<WebStoreDB>()
               .AddDefaultTokenProviders();

            #region WebAPI Identity clients stores

            services
               .AddTransient<IUserStore<User>, UsersClient>()
               .AddTransient<IUserPasswordStore<User>, UsersClient>()
               .AddTransient<IUserEmailStore<User>, UsersClient>()
               .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
               .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
               .AddTransient<IUserLockoutStore<User>, UsersClient>()
               .AddTransient<IUserClaimStore<User>, UsersClient>()
               .AddTransient<IUserLoginStore<User>, UsersClient>();
            services
               .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCD...123457890";
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<ICartService, CookiesCartService>();
            services.AddScoped<IOrderService, OrdersClient>();

            //регистрируем клиента как сервис 
            services.AddScoped<IValuesService, ValuesClient>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
