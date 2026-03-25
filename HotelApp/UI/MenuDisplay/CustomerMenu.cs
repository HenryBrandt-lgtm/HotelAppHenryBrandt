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
        public static void ShowCustomerMenu(CustomerService customerService)
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                

                var option = ScrollMenu.ScrollingMenu(MenuOptions.CustomerMenu());
                Console.CursorVisible = true;

                switch (option)
                {
                    case 0:
                        Console.Clear();
                        customerService.ReadCustomer();
                        break;
                    case 1:
                        //add
                        break;
                    case 2:
                        Console.Clear();
                        customerService.RemoveCostumerById();
                        break;
                    case 3:
                        //update Customer
                        break;
                    case 4:
                        Console.WriteLine("Going back to Main Menu");
                        Thread.Sleep(1000);
                        Console.Clear();
                        exitMenu = true;
                        break;
                }
            }
        }
    }
}
