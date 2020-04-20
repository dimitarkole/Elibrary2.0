namespace ELibrary.Web
{
    using System;
    using System.Reflection;

    using ELibrary.Data;
    using ELibrary.Data.Common;
    using ELibrary.Data.Common.Repositories;
    using ELibrary.Data.Models;
    using ELibrary.Data.Repositories;
    using ELibrary.Data.Seeding;
    using ELibrary.Services.Admin;
    using ELibrary.Services.BaseServices;
    using ELibrary.Services.CommonResurcesServices;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Services.Data;
    using ELibrary.Services.LibraryServices;
    using ELibrary.Services.Mapping;
    using ELibrary.Services.Messaging;
    using ELibrary.Services.UserServices;
    using ELibrary.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;

                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();

            // Admin services
            services.AddTransient<IAddGenreService, AddGenreService>();
            services.AddTransient<IAllAddedGenresService, AllAddedGenresService>();
            services.AddTransient<IAllUsersService, AllUsersService>();
            services.AddTransient<IStatsAdminService, StatsAdminService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAddPaymentPlanService, AddPaymenPlantService>();
            services.AddTransient<IAllPaymentPlansService, AllPaymentPlansService>();

            // common services
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ISendMail, SendMail>();

            // bases services

            services.AddTransient<IViewBooksService, ViewBooksService>();
            services.AddTransient<IViewBookService, ViewBookService>();
            services.AddTransient<IAllLibrariesService, AllLibrariesService>();
            services.AddTransient<IViewLibraryService, ViewLibraryService>();

            // Library services
            services.AddTransient<IAddBookService, AddBookService>();
            services.AddTransient<IGiveBookService, GiveBookService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGivenBooksService, GivenBooksService>();
            services.AddTransient<IAllAddedBooksService, AllAddedBooksService>();
            services.AddTransient<IStatsLibraryService, StatsLibraryService>();

            // User services
            services.AddTransient<ITakenBooksService, TakenBooksService>();
            services.AddTransient<IStatsUserService, StatsUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
        }
    }
}
