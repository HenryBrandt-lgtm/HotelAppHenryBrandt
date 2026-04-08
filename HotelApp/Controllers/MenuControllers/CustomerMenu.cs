using HotelApp.Interface;
using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers.MenuControllers
{
    public class CustomerMenu(CustomerController controller) : IMenu
    {
       
        public void ShowMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                var menuItems = MenuOptions.CustomerMenu(controller, () =>

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
