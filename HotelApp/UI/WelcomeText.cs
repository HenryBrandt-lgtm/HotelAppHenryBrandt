using Spectre.Console;

namespace HotelApp.UI
{
    public class WelcomeText
    {
        public static void WelcomeScreen()
        {
            var welcomeTo = new FigletText("Welcome to")
            {
                Color = Color.Cyan,
                Justification = Justify.Center
            };

            var header = HeaderDisplay.GetHeader();

            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            

            AnsiConsole.Write(welcomeTo);
            AnsiConsole.Write(header);
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
    }
}
