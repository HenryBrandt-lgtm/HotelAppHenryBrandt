using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class MainMenu(BookingService bookingService, BookingMenu bookingMenu, CustomerMenu customerMenu, RoomMenu roomMenu) : IMenu
    {
        public void ShowMenu()
        {

            bool exitMenu = false;
            while (!exitMenu)
            {

                var option = ScrollMenu.ShowScrollingMenu(MenuOptions.MainMenu());

                switch (option)
                {
                    case 0:
                        bookingService.ReadAvailableRooms();
                        break;
                    case 1:
                        bookingService.Create();
                        break;
                    case 2:
                        Console.Clear();
                        bookingMenu.ShowMenu();
                        break;
                    case 3:
                        Console.Clear();
                        customerMenu.ShowMenu();
                        break;
                    case 4:
                        Console.Clear();
                        roomMenu.ShowMenu();
                        break;
                    case 5:
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

