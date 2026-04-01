using HotelApp.MenuData;
using HotelApp.Services;
using HotelApp.UI.MenuDisplay;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Controllers
{
    public class RoomMenu
    {
        private readonly RoomService _roomService;

        public RoomMenu(RoomService roomService)
        {
            _roomService = roomService;
        }
        public void ShowRoomMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.RoomMenu(_roomService,
                    () =>
                    {
                        AnsiConsole.MarkupLine("[red]Går tillbaka till huvudmenyn...[/]");
                        Thread.Sleep(1000);
                        exitMenu = true;
                    });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(titles);

                menuItems[option].Action.Invoke();
            }
        }
    }
}
