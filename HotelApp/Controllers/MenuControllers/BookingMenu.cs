using HotelApp.Interface;
using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers.MenuControllers
{
    public class BookingMenu(BookingService bookingService) : IMenu
    {
        
        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.BookingMenu(bookingService, () =>
                     {
                         AnsiConsole.MarkupLine("[red]Går tillbaka till huvudmenyn...[/]");
                         Thread.Sleep(1000);
                         exitMenu = true;
                     });
                var items = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(items);

                menuItems[option].Action.Invoke();
            }
        }
    }
}
