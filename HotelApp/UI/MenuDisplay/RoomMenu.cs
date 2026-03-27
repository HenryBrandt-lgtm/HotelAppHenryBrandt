using HotelApp.MenuData;
using HotelApp.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.UI.MenuDisplay
{
    internal class RoomMenu
    {
        public static void ShowRoomMenu()
        {
            ICrud roomCrud = new RoomService();
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.RoomMenu(roomCrud,
                    () =>
                    {
                        var terminating = new Text("Går tillbaka till huvudmenyn...", new Style(Color.Red))
                        {
                            Justification = Justify.Center
                        };

                        AnsiConsole.Write(terminating);
                        Thread.Sleep(1000);
                        exitMenu = true;
                    });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ScrollingMenu(titles);

                Console.CursorVisible = true;

                menuItems[option].Action.Invoke();
            }
        }
    }
}
