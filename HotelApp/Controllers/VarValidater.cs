using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HotelApp.Controllers
{
    public class VarValidater
    {
        public static string GetRequiredString(string message)
        {
            string input;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Får inte vara tomt. Försök igen.");
                }

            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        public static DateOnly GetValidDate(string message)
        {
            DateOnly birthday = default;
            while (true)
            {
                Console.Write("Skriv in nytt person.nr (yyyy-MM-dd): ");
                if (DateOnly.TryParseExact(Console.ReadLine(),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    if (birthday >= DateOnly.FromDateTime(DateTime.Today))
                        Console.WriteLine("Personen måste vara äldre än dagens datum");

                    else
                        return birthday;
                    
                }
                else
                    Console.WriteLine("Ogiltigt format, vänligen försök igen: yyyy-MM-dd");

            }
        }
    }
}
