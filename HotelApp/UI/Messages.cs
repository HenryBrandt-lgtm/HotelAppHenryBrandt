using Spectre.Console;

namespace HotelApp.UI
{
    public class Messages
    {
        public static void WaitForKey()
        {
            var pressAnyKeyMessage = GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public static Text GetPressAnyKeyText(string message = "Tryck på valfri knapp för att fortsätta", Color? color = null)
        {
            var finalColor = color ?? Color.Green;

            return new Text(message, new Style(finalColor))
            {
                Justification = Justify.Center
            };

        }
    }
}
