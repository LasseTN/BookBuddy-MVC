using BookBuddy.BusinessLogicLayer.Interface;
using BookBuddy.BusinessLogicLayer;
using BookBuddy.ServiceLayer;
using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace BookBuddy_MVC {
    public class Program {
        public static void Main(string[] args) {

            try {
                var builder = WebApplication.CreateBuilder(args);



                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>();
                builder.Services.AddControllersWithViews();

                // Mine services.

                builder.Services.AddTransient<ServiceConnection>();
                builder.Services.AddTransient<IBookControl, BookControl>();
                builder.Services.AddTransient<IBookAccess, BookAccess>();
                builder.Services.AddHttpClient("API", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7199/api/"); // Replace with your actual API base URL
                });


                // Initialize Serilog
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .WriteTo.File(new CompactJsonFormatter(), "Logs/APILog-.json", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                // Use Serilog for logging in the application
                builder.Host.UseSerilog();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment()) {
                    app.UseMigrationsEndPoint();
                } else {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

                app.Run();
            } catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
            } finally {
                // Ensure any buffered events are sent at shutdown
                Log.CloseAndFlush();
            }
        }
    }
}
