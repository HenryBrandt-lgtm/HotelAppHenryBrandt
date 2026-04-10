using HotelApp.Controllers.MenuControllers;
using HotelApp.Data;
using HotelApp.UI;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApp
{
    public class App
    {
        public static void RunProgram()
        {

            var config = AppConfiguration.BuildConfiguration();

            using var provider = AppConfiguration.BuildServiceProvider(config);
            using var scope = provider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
            var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();

            initializer.MigrateAndSeed(db);

            WelcomeText.WelcomeScreen();
            mainMenu.ShowMenu();

        }
    }
}
