using HotelApp.MenuData;
using HotelApp.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.UI.MenuDisplay
{
    public class BookingMenu
    {
        public static void ShowBookingMenu()
        {
            ICrud bookingCrud = new BookingService();
            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ScrollingMenu(MenuOptions.BookingMenu());
                Console.CursorVisible = true;

                var actions = new List<Action>
                {
                    bookingCrud.Read,
                    bookingCrud.Update,
                    bookingCrud.Delete,
                    () =>
                    {
                        var terminating = new Text("Going back to MainMenu...", new Style(Color.Red))
                        {
                            Justification = Justify.Center
                        };

                        AnsiConsole.Write(terminating);
                        Thread.Sleep(1000);
                        exitMenu = true;
                    }
                };

                actions[option].Invoke();
            }
        } 
    }
}
