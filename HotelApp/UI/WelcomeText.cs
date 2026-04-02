using Spectre.Console;

namespace HotelApp.UI
{
    public class WelcomeText
    {
        public static void WelcomeScreen()
        {
            var welcomeTo = new FigletText("Välkommen till")
            {
                Color = Color.Cyan,
                Justification = Justify.Center
            };

            var header = HeaderDisplay.GetHeader();           

            AnsiConsole.Write(welcomeTo);
            AnsiConsole.Write(header);
            
            Messages.WaitForKey();
        }
    }
}
