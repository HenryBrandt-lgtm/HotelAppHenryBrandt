using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class CustomerMenu
    {
        private readonly CustomerService _customerService;

        public CustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }
        public void ShowCustomerMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                var menuItems = MenuOptions.CustomerMenu(_customerService, () =>

                {
                    AnsiConsole.MarkupLine("[red]Går tillbaka till huvudmenyn...[/]");
                    Thread.Sleep(1000);
                    exitMenu = true;
                });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(titles);

                menuItems[option].Action.Invoke();
            }
        }
    }
}
