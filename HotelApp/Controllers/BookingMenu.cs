using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class BookingMenu
    {
        private readonly BookingService _bookingService;

        public BookingMenu(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public void ShowBookingMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.BookingMenu(_bookingService, () =>
                     {
                         AnsiConsole.MarkupLine("[red]Går tillbaka till huvudmenyn...[/]");
                         Thread.Sleep(1000);
                         exitMenu = true;
                     });
                var items = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(items);

                Console.CursorVisible = true;

                menuItems[option].Action.Invoke();
            }
        }
    }
}
