using Spectre.Console;

namespace HotelApp.UI
{
    public class HeaderDisplay
    {
        public static FigletText GetHeader(string message = "Henrys Hotel")
        {

            var header = new FigletText(message)
            {
                Color = Color.Cyan,
                Justification = Justify.Center
            };
            return header;
        }
    }
}
