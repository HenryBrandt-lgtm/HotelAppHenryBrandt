using HotelApp.Interface;
using HotelApp.MenuData;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers.MenuControllers
{
    public class MainMenu(BookingMenu bookingMenu, BookingController bookingController,
        CustomerMenu customerMenu, RoomMenu roomMenu, CustomerController customerController) : IMenu
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
                        bookingController.ReadAvailableRooms();
                        break;
                    case 1:
                        bookingController.Create();
                        break;
                    case 2:
                        bookingController.Pay();
                        break;
                    case 3:
                        Console.Clear();
                        bookingMenu.ShowMenu();
                        break;
                    case 4:
                        Console.Clear();
                        customerMenu.ShowMenu();
                        break;
                    case 5:
                        Console.Clear();
                        roomMenu.ShowMenu();
                        break;
                    case 6:
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

