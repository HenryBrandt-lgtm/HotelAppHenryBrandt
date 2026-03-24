using HotelApp.UI;
using Spectre.Console;
using System;

namespace HotelApp.Models.MenuModels
{
    public class ScrollMenu
    {
        public static int ScrollingMenu(List<string> options)
        {
            ConsoleKeyInfo key;
            int select = 0;
            Console.CursorVisible = false;
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(HeaderDisplay.GetHeader());
                AnsiConsole.WriteLine();

                for (int i = 0; i < options.Count; i++)
                {
                    string indicator = "";
                    if (i == select)
                        indicator = "> ";
                    var line = new Text(indicator + options[i], i == select ? new Style(Color.Green) : null)
                    {
                        Justification = Justify.Center
                    };                   

                    AnsiConsole.Write(line);

                }
               
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        select = select == options.Count - 1 ? 0 : select + 1;
                        break;

                    case ConsoleKey.UpArrow:
                        select = select == 0 ? options.Count - 1 : select - 1;
                        break;

                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return select;
                        
                        
                }
            }
        }             
    }
}
