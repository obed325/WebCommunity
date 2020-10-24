using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCommunity.Data;
using WebCommunity.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using WebCommunity.Services;
using WebCommunity.Hubs;
using WebCommunity.Helpers;

namespace WebCommunity
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

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDefaultIdentity<ApplicationUser>(
                options => options.SignIn.RequireConfirmedAccount = false)//todo change to true before deploy
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ0123456789-._@+"
                );

            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));

            //services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    
                    //.RequireRole("Admin")//added 10/7
                    .Build();
            });

            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IDeleteImages, DeleteImages>();
            services.AddScoped<IForum, WebCommunity.Services.ForumService>();
            services.AddScoped<IPost, PostService>();
            services.AddScoped<IPostReply, PostReplyService>();
            services.AddScoped<IApplicationUser, ApplicationUserService>();
            //services.AddScoped<IPostFormatter, PostFormatter>();
            //services.AddSingleton<IUpload, UploadService>();
            services.AddSignalRCore();
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllerRoute(
                    name: "chat",
                    pattern: "{controller=Chat}/{action=Chat}/{id?}");
            });

            

            //create an admin if not exists
            CreateUserRoles(serviceProvider).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Add Admin role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if(!roleCheck)
            {
                //create role (or roles) and seed to db
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //assign Admin role to the main user we have given our newly registred
            //login id for Admin management
            ApplicationUser user = await UserManager.FindByEmailAsync("obed325@hotmail.com");
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
