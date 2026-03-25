using HotelApp.MenuData;
using HotelApp.Services;
using kassasystem;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace HotelApp.UI.MenuDisplay
{
    public class CustomerMenu
    {
        public static void ShowCustomerMenu()
        {
            ICrud customerCrud = new CustomerService();
            bool exitMenu = false;
            while (!exitMenu)
            {        
                var option = ScrollMenu.ScrollingMenu(MenuOptions.CustomerMenu());
                Console.CursorVisible = true;

                var actions = new List<Action>
                {
                    customerCrud.Create,
                    customerCrud.Read,
                    customerCrud.Update,
                    customerCrud.Delete,
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
