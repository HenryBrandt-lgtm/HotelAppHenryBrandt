using HotelApp.Data;
using HotelApp.Services;
using HotelApp.UI;
using kassasystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace HotelApp
{
    public static class App
    {
        public static void RunProgram()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                dbContext.Database.Migrate();
            }

            var customerService = new CustomerService();
            customerService.SeedCustomers();

            WelcomeText.WelcomeScreen();
            MainMenu.ShowMainMenu(customerService);
        }
    }
}
