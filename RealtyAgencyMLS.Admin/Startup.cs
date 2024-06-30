using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealtyAgencyMLS.Admin.Helpers;
using RealtyAgencyMLS.BAL.EmailServices;
using RealtyAgencyMLS.BAL.Filters;
using RealtyAgencyMLS.BAL.Services.Implementation;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.DAL.Connection;
using RealtyAgencyMLS.DAL.Data;
using RealtyAgencyMLS.DAL.Repository;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.Common;
using System;

namespace RealtyAgencyMLS.Admin
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
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 52428800;

            });
            services.AddMvc(options => options.Filters.Add(typeof(DynamicAuthorizationFilter)));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IUserManageServices, UserManageServices>();
            services.AddSingleton<IMvcControllerDiscovery, MvcControllerDiscovery>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           // services.ConfigureApplicationCookie(options =>
           //{
           //    options.LoginPath = $"/Account/Login";
           //    options.LogoutPath = $"/Account/Logout";
           //    options.AccessDeniedPath = $"/Account/AccessDenied";
           //});
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
            
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
