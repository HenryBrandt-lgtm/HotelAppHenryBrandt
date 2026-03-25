using HotelApp.MenuData;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.UI.MenuDisplay
{
    public class BookingMenu
    {
        public static void ShowBookingMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ScrollingMenu(MenuOptions.BookingMenu());
                Console.CursorVisible = true;

                switch (option)
                {
                    case 0:
                        //view bookings
                        break;
                    case 1:
                        //remove bookings
                        break;
                    case 2:
                        // update bookings
                        break;
                    case 3:
                        Console.WriteLine("Going Back To Main Menu");
                        Thread.Sleep(1000);
                        Console.Clear();
                        exitMenu = true;
                        break;
                }
            }
        } 
    }
}
