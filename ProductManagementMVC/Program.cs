using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using DataAccessObjects;
using System.Configuration;

namespace ProductManagementMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionStringDB");

            builder.Services.AddDbContext<MystoreContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<CategoryDAO>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();


            builder.Services.AddScoped<AccountDAO>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<ProductDAO>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout
                options.Cookie.HttpOnly = true; // For security
                options.Cookie.IsEssential = true; // Ensure session cookie is always created
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
