using AutoMapper;
using Demo.BLL.Interface;
using Demo.BLL.Repository;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Demo.PL.mapingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
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
            services.AddControllersWithViews();

            //services.AddDbContext<MvcAppDBContext>(); 

            services.AddDbContext<MvcAppDBContext>(option =>

            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))   );

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
             
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddAutoMapper(M => M.AddProfiles(new List<Profile> { new EmployeeProfile(), new UserProfile() ,new RoleProfile()})) ;

            services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddIdentity<AppLication_User, IdentityRole>()
				  .AddEntityFrameworkStores<MvcAppDBContext>()
                  .AddDefaultTokenProviders();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options=>
                {
                    options.LoginPath = "Acount/Login";
                    options.AccessDeniedPath = "Home/Error";
                    
                });
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                    pattern: "{controller=Acount}/{action=Login}/{id?}");
            });
        }
    }
}
