using Spectre.Console;

namespace HotelApp.UI
{
    internal class Messages
    {
        public static Text GetPressAnyKeyText(string message = "press any key to Continue", Color? color = null)
        {
            var finalColor = color ?? Color.Green;

            return new Text(message, new Style(finalColor))
            {
                Justification = Justify.Center
            };

        }
    }
}
