using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Services;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApp
{
    class App
    {
        public static void RunProgram()
        {
            // load config
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // services
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<DataInitializer>();
            services.AddScoped<BookingService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<RoomService>();
            services.AddScoped<BookingMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<RoomMenu>();

            // ... other services

            using var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
            var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();
            var customerMenu = scope.ServiceProvider.GetRequiredService<CustomerMenu>();
            var roomMenu = scope.ServiceProvider.GetRequiredService<RoomMenu>();
            var bookingMenu = scope.ServiceProvider.GetRequiredService<BookingMenu>();

            initializer.MigrateAndSeed(db);

            // proceed to run UI
            WelcomeText.WelcomeScreen();
            mainMenu.ShowMainMenu(bookingMenu, customerMenu, roomMenu);
        }
    }
}
