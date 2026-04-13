using HotelApp.Enums;
using Spectre.Console;
using System.Globalization;

namespace HotelApp.UI
{
    public class VarValidater
    {
        public static int GetRequiredIntOverZero(string message)
        {
            int value;
            string input;

            while (true)
            {
                input = AnsiConsole.Ask<string>(message);

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("Får inte vara tomt. Försök igen.")));
                    continue;
                }

                if (int.TryParse(input, out value) && value > 0)
                {
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt tal. Ange ett heltal över 0, t.ex. 2.")));
                }
            }
        }
        public static int GetRequiredInt(string message)
        {
            int value;
            string input;

            while (true)
            {
                input = AnsiConsole.Ask<string>(message);

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("Får inte vara tomt. Försök igen.")));
                    Messages.WaitForKey();
                    continue;
                }

                if (int.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("Ogiltigt tal. Ange ett heltal över 0, t.ex. 2.")));
                    Messages.WaitForKey();
                }
            }
        }
        public static decimal GetRequiredDecimalOverZero(string message)
        {
            decimal value;
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Får inte vara tomt.[/] Försök igen.")));
                    continue;
                }

                if (decimal.TryParse(input, out value) && value > 0)
                {
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Ogiltigt tal. Ange ett decimaltal över 0, tex 123.45.[/]")));
                }
            }
        }
        public static string GetRequiredStringMax25(string message)
        {
            string input;

            do
            {
                input = AnsiConsole.Ask<string>(message);

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Får inte vara tomt.[/] Försök igen.")));
                }
                else if (input.Length < 2)
                {
                    AnsiConsole.Write(Align.Center(new Markup("För kort input.")));
                    continue;
                }
                else if (input.Length > 25)
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]För långt input. Max 25 tecken.[/] Försök igen.")));
                }

            } while (input.Length < 2 || input.Length > 25);

            return input;
        }
        public static DateOnly GetValidDateOnly(string message)
        {
            int maxAge = 100;
            int minAge = 18;
            var today = DateOnly.FromDateTime(DateTime.Today);
            var minBirthday = today.AddYears(-maxAge);
            var maxBirthday = today.AddYears(-minAge);

            DateOnly birthday = default;
            while (true)
            {

                Console.Write(message);
                if (DateOnly.TryParseExact(Console.ReadLine(),
                    "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    if (birthday >= maxBirthday)
                        AnsiConsole.Write(Align.Center(new Markup("Personen måste vara äldre än dagens 18år för att kunna boka")));

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
        public static int GetRequiredExtraBeds(string message, int area, int beds)
        {
            int value;
            string input;

            while (true)
            {
                input = AnsiConsole.Ask<string>(message);

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Får inte vara tomt.[/] Försök igen.")));
                    continue;
                }

                if (int.TryParse(input, out value) && value >= 0 && value <= 2)
                {
                    if (area < 30 && value > 1)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Fler extrasängar än 1 kräver att rummet är minst 30 kvm.[/] Försök igen.")));
                        continue;
                    }
                    else if (area < 20  && value > 0 && beds < 2)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]En extrasäng kräver att rummet är minst 20 kvm och ett dubbelrum.[/] Försök igen.")));
                        continue;
                    }
                    return value;
                }
                else
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Ogiltigt tal. Ange ett heltal över 0, t.ex. 2.[/]")));
                }
            }
        }        
    }
}
