using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Controllers
{
    public class BookingMenu
    {
        public static void ShowBookingMenu()
        {
            ICrud bookingCrud = new BookingService();
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.BookingMenu(bookingCrud, () =>
                     {
                         var terminating = new Text("Går tillbaka till huvudmenyn...", new Style(Color.Red))
                         {
                             Justification = Justify.Center
                         };

                         AnsiConsole.Write(terminating);
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
