using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Controllers
{
    public class MainMenu(BookingService bookingService, BookingMenu bookingMenu, CustomerMenu customerMenu, RoomMenu roomMenu) : IMenu
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

        public void ShowMenu()
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
                        bookingMenu.ShowMenu();
                        break;
                    case 2:
                        Console.Clear();
                        customerMenu.ShowMenu();
                        break;
                    case 3:
                        Console.Clear();
                        roomMenu.ShowMenu();
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

