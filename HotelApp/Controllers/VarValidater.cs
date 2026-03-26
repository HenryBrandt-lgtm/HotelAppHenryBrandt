using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HotelApp.Controllers
{
    public class VarValidater
    {
        public static int GetRequierdInt (string message)
        {
            int value;
            string input;

            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Får inte vara tomt. Försök igen.");
                    continue;
                }

                if (int.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Ogiltigt tal. Ange ett heltal, t.ex. 5.");
                }
            }
        }
        public static decimal GetRequiredDecimal(string message)
        {
            decimal value;
            string input;
            while(true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Får inte vara tomt. Försök igen.");
                    continue;
                }

                if (decimal.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Ogiltigt tal. Ange ett decimaltal, tex 123.45.");
                }
            }
        }
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
            int maxAge = 100;
            var today = DateOnly.FromDateTime(DateTime.Today);
            var minBirthday = today.AddYears(-maxAge);

            DateOnly birthday = default;
            while (true)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("Får inte vara tomt. Försök igen.");
                    continue;
                }
                Console.Write("Skriv in nytt person.nr (yyyy-MM-dd): ");
                if (DateOnly.TryParseExact(Console.ReadLine(),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    if (birthday >= today)
                        Console.WriteLine("Personen måste vara äldre än dagens datum");

                    else if (birthday < minBirthday)
                        Console.WriteLine("Personen kan nog inte vara så gammal");
                    else
                        return birthday;
                    
                }
                else
                    Console.WriteLine("Ogiltigt format, vänligen försök igen: yyyy-MM-dd");

            }
        }
    }
}
