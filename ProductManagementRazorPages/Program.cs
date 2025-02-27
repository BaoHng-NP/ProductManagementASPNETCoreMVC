using Services;
using Repositories;
using DataAccessObjects;
using SignalRLab;

namespace ProductManagementRazorPages
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddRazorPages();

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
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapHub<SignalRServer>("/signalRServer");
            app.MapRazorPages();

            app.Run();
        }
    }
}
