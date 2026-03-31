using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Controllers
{
    public class MainMenu
    {
        private readonly BookingService _bookingService;
        private readonly BookingMenu _bookingMenu;
        private readonly CustomerMenu _customerMenu;
        private readonly RoomMenu _roomMenu;

        public MainMenu(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        public void ShowMainMenu(BookingMenu bookingMenu, CustomerMenu customerMenu, RoomMenu roomMenu)
        {

            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ShowScrollingMenu(MenuOptions.MainMenu());
                Console.CursorVisible = true;

                switch (option)
                {
                    case 0:
                        _bookingService.Create();
                        break;
                    case 1:
                        Console.Clear();
                        bookingMenu.ShowBookingMenu();
                        break;
                    case 2:
                        Console.Clear();
                        customerMenu.ShowCustomerMenu();
                        break;
                    case 3:
                        Console.Clear();
                        roomMenu.ShowRoomMenu();
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

