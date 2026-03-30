using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class CustomerMenu
    {
        public static void ShowCustomerMenu()
        {
            ICrud customerCrud = new CustomerService();
            bool exitMenu = false;

            while (!exitMenu)
            {
                var menuItems = MenuOptions.CustomerMenu(customerCrud, () =>

                {
                    var terminating = new Text("Går tillbaka till huvudmenyn...", new Style(Color.Red))
                    { Justification = Justify.Center };

                    AnsiConsole.Write(terminating);
                    Thread.Sleep(1000);
                    exitMenu = true;
                });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(titles);

                Console.CursorVisible = true;

                menuItems[option].Action.Invoke();
            }
        }
    }
}
