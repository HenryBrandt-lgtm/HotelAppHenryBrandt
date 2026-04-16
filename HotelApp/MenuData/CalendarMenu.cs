using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.MenuData
{
    public class CalendarMenu
    {
        public static void RenderCalendar(DateTime selectedDate, string message)
        {
            var calendarContent = new StringWriter();

            calendarContent.WriteLine($"[red]{selectedDate:MMMM}[/]".ToUpper());
            calendarContent.WriteLine("Mån  Tis  Ons  Tor  Fre  Lör  Sön");
            calendarContent.WriteLine("─────────────────────────────────");

            DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
            int startDay = (int)firstDayOfMonth.DayOfWeek;
            startDay = (startDay == 0) ? 6 : startDay - 1;

            for (int i = 0; i < startDay; i++)
            {
                calendarContent.Write("     ");
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                if (day == selectedDate.Day)
                {
                    calendarContent.Write($"[green]{day,2}[/]   ");
                }
                else
                {
                    calendarContent.Write($"{day,2}   ");
                }

                if ((startDay + day) % 7 == 0)
                {
                    calendarContent.WriteLine();
                }
            }

            var panel = new Panel(calendarContent.ToString())
            {
                Border = BoxBorder.Double,
                Header = new PanelHeader(($"[red]{selectedDate:yyyy}[/]"), Justify.Center)
            };

            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            var content = new Rows(panel, new Markup($"\nAnvänd piltangenter [cyan]< > ^ v[/] för att " +
               $"\nnavigera och [green]Enter[/] för att välja [yellow]{message}[/]."));
            AnsiConsole.Write(Align.Center(content));
            Console.CursorVisible = false;

        }
        public static DateTime CalendarController(string message)
        {
            DateTime currentDate = DateTime.Now;
            DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, 1);


            while (true)
            {
                Console.Clear();
                RenderCalendar(selectedDate, message);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        selectedDate = selectedDate.AddDays(1);
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedDate = selectedDate.AddDays(-1);
                        break;
                    case ConsoleKey.UpArrow:
                        selectedDate = selectedDate.AddDays(-7);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedDate = selectedDate.AddDays(7);
                        break;
                    case ConsoleKey.Enter:
                        AnsiConsole.Write(Align.Center(new Markup($"\nDu valde: [green]{selectedDate:yyyy-MM-dd}[/]")));
                        Messages.WaitForKey();
                        return selectedDate; 

                    case ConsoleKey.Escape:
                        break; 
                }
            }
        }

    }
}
