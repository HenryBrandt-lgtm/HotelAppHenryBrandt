using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HotelApp.Controllers
{
    public class VarValidater
    {
        public static int GetRequiredInt (string message)
        {
            int value;
            string input;

            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("Får inte vara tomt. Försök igen.")));
                    continue;
                }

                if (int.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt tal. Ange ett heltal, t.ex. 5.")));
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
                    AnsiConsole.Write(Align.Center(new Markup("Får inte vara tomt. Försök igen.")));
                    continue;
                }

                if (decimal.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt tal. Ange ett decimaltal, tex 123.45.")));
                }
            }
        }
        public static string GetRequiredString(string message)
        {
            string input;

            do
            {
                AnsiConsole.Write(Align.Center(new Markup(message)));
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Får inte vara tomt.[/] Försök igen.")));
                }

            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        public static DateOnly GetValidDateOnly(string message)
        {
            int maxAge = 100;
            var today = DateOnly.FromDateTime(DateTime.Today);
            var minBirthday = today.AddYears(-maxAge);

            DateOnly birthday = default;
            while (true)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Får inte vara tomt.[/] Försök igen.")));
                    continue;
                }
                Console.Write(message);
                if (DateOnly.TryParseExact(Console.ReadLine(),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    if (birthday >= today)
                        AnsiConsole.Write(Align.Center(new Markup("Personen måste vara äldre än dagens datum")));

                    else if (birthday < minBirthday)
                        AnsiConsole.Write(Align.Center(new Markup("Personen kan nog inte vara så gammal")));
                    else
                        return birthday;
                    
                }
                else
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt format, vänligen försök igen: yyyy-MM-dd")));

            }
        }
        public static DateOnly GetValidBookingDate(string message)
        {
            int maxDate = 10;
            var today = DateOnly.FromDateTime(DateTime.Today);
            var lateDate = today.AddYears(+maxDate);

            DateOnly birthday = default;
            while (true)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    AnsiConsole.Write(Align.Center(new Markup("Får inte vara tomt. Försök igen.")));
                    continue;
                }
                Console.Write(message);
                if (DateOnly.TryParseExact(Console.ReadLine(),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    if (birthday < today)
                        AnsiConsole.Write(Align.Center(new Markup("Du kan inte boka ett datum bakåt i tiden")));

                    else if (birthday > lateDate)
                        AnsiConsole.Write(Align.Center(new Markup("Vi tar inte boknignar så långt in i framtiden")));
                    else
                        return birthday;

                }
                else
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt format, vänligen försök igen: yyyy-MM-dd")));

            }
        }        
    }
}
