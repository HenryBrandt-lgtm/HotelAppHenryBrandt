using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace kassasystem
{
    public class MainMenu
    {
        public static void ShowMainMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ScrollingMenu(MenuOptions.MainMenu());
                Console.CursorVisible = true;

                switch (option)
                {
                    case 0:
                        var bookingService = new BookingService();
                        bookingService.Create();
                        break;
                    case 1:
                        Console.Clear();
                        BookingMenu.ShowBookingMenu();
                        break;
                    case 2:
                        Console.Clear();
                        CustomerMenu.ShowCustomerMenu();
                        break;
                    case 3:
                        Console.Clear();
                        RoomMenu.ShowRoomMenu();
                        break;
                    case 4:
                        var terminating = new Text("Terminating...", new Style(Color.Red))
                        {
                            Justification = Justify.Center
                        };
                        AnsiConsole.Write(terminating);
                        Thread.Sleep(1000);
                        exitMenu = true;
                        break;
                }
            }
        } 
    }
}

