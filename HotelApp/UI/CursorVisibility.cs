using HotelApp.Services.CreateBookingServices;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.UI
{
    public class CursorVisibility
    {
        public static void WaitForKey()
        {
            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
    }
}
