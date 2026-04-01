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

            // skapar upp en serviceCollection och fyller den med alla services som behövs i applikationen,
            // inklusive DbContext och menyer
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<DataInitializer>();
            services.AddScoped<BookingService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<RoomService>();
            services.AddScoped<BookingMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<RoomMenu>();

            // skapar upp en serviceProvider och en scope för att kunna använda de services som registrerats,
            // inklusive databasinitialisering,
            // kan tex skapa upp controllers och skicka genom appen

            using var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
            var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();

            // Skapar upp databasen med hjälp av migration filerna och seedar databasen med datan jag skapat upp (4pers och 4rum)
            initializer.MigrateAndSeed(db);

            // proceed to run UI
            WelcomeText.WelcomeScreen();
            mainMenu.ShowMainMenu();
        }
    }
}
