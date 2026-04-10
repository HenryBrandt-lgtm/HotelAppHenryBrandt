using HotelApp.Enums;
using Spectre.Console;

namespace HotelApp.UI
{
    public class ScrollMenu
    {
        public static int ShowScrollingMenu(List<string> options)
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
                    string indicator = "  ";
                    if (i == select)
                        indicator = "> ";
                    var line = new Text(indicator + options[i], i == select ? new Style(Color.Green) : null)
                    {
                        Justification = Justify.Center
                    };

                    AnsiConsole.Write(line);

                }
                AnsiConsole.Write(Align.Center(new Markup($"\nAnvänd piltangenter [cyan]^ v[/] för att " +
               $"\nnavigera och [green]Enter[/] för att välja.")));

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
        public static RoomType ShowScrollingEnumMenu(List<RoomType> options)
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
                    string indicator = "  ";
                    if (i == select)
                        indicator = "> ";
                    var line = new Text(indicator + options[i].GetDescription(), i == select ? new Style(Color.Green) : null)
                    {
                        Justification = Justify.Center
                    };

                    AnsiConsole.Write(line);

                }
                AnsiConsole.Write(Align.Center(new Markup($"\nAnvänd piltangenter [cyan]^ v[/] för att " +
               $"\nnavigera och [green]Enter[/] för att välja.")));

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

                        Console.Clear();
                        AnsiConsole.Write(Align.Center(new Markup($"[green]Du har valt {options[select].GetDescription()}![/]")));
                        Messages.WaitForKey();
                        return options[select];
                }
            }
        }
    }
}
