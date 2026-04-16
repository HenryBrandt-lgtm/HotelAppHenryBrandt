using HotelApp.Interface;
using HotelApp.MenuData;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers.MenuControllers
{
    public class BookingMenu(BookingController controller) : IMenu
    {

        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.BookingMenu(controller, () =>
                     {
                         exitMenu = true;
                     });
                var items = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(items);

                menuItems[option].Action.Invoke();
            }
        }
    }
}
