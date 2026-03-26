using HotelApp.MenuData;
using HotelApp.Services;
using Spectre.Console;

namespace HotelApp.UI.MenuDisplay
{
    public class CustomerMenu
    {
        public static void ShowCustomerMenu()
        {
            ICrud customerCrud = new CustomerService();
            bool exitMenu = false;

            while (!exitMenu)
            {
                var menuItems = MenuOptions.CustomerMenu(customerCrud,() =>
                {var terminating = new Text("Going back to MainMenu...", new Style(Color.Red))
                        { Justification = Justify.Center };

                        AnsiConsole.Write(terminating);
                        Thread.Sleep(1000);
                        exitMenu = true;
                    });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ScrollingMenu(titles);

                Console.CursorVisible = true;

                menuItems[option].Action.Invoke();
            }
        }
    }
}
