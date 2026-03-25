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
                var option = ScrollMenu.ScrollingMenu(MenuOptions.RoomMenu());
                Console.CursorVisible = true;

                var actions = new List<Action>
                {
                    roomCrud.Create,
                    roomCrud.Read,
                    roomCrud.Update,
                    roomCrud.Delete,
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
