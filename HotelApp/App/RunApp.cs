using HotelApp.Services;
using HotelApp.UI;
using kassasystem;


namespace HotelApp.App
{
    public static class RunApp
    {
        public static void RunProgram()
        {
            var customerService = new CustomerService();
            customerService.SeedCustomers();

            WelcomeText.WelcomeScreen();
            MainMenu.ShowMainMenu(customerService);
        }
    }
}
