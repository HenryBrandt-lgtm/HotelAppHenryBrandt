using HotelApp.Interface;
using HotelApp.MenuData;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers.MenuControllers
{
    public class RoomMenu(RoomController roomController) : IMenu
    {

        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                var menuItems = MenuOptions.RoomMenu(roomController,
                    () =>
                    {
                        exitMenu = true;
                    });

                var titles = menuItems.Select(m => m.Title).ToList();

                var option = ScrollMenu.ShowScrollingMenu(titles);

                menuItems[option].Action.Invoke();
            }
        }
    }
}
