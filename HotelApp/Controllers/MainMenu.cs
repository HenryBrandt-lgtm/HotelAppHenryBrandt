using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Controllers
{
    public class MainMenu(BookingService bookingService, BookingMenu bookingMenu, CustomerMenu customerMenu, RoomMenu roomMenu)
    {
        //private readonly BookingService _bookingService;
        //private readonly BookingMenu _bookingMenu;
        //private readonly CustomerMenu _customerMenu;
        //private readonly RoomMenu _roomMenu;

        //public MainMenu(BookingService bookingService, BookingMenu bookingMenu, CustomerMenu customerMenu, RoomMenu roomMenu)
        //{
        //    _bookingService = bookingService;
        //    _bookingMenu = bookingMenu;
        //    _customerMenu = customerMenu;
        //    _roomMenu = roomMenu;
        //}
        
        public void ShowMainMenu()
        {

            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ShowScrollingMenu(MenuOptions.MainMenu());

                switch (option)
                {
                    case 0:
                        bookingService.Create();
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

