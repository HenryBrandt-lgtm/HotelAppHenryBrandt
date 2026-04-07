using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services.CreateBookingServices
{
    public class BookingDate
    {
        public (DateOnly StartDate, DateOnly EndDate, int NumberOfGuests) SetVisitingConditions()
        {

            while (true)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateTime start = CalendarMenu.CalendarController("Inceckningsdatum");
                DateOnly startDate = DateOnly.FromDateTime(start);
                if (startDate < today)
                {
                    Console.Clear();
                    AnsiConsole.Write(Align.Center(new Markup("[red]Incheckning kan inte vara före dagens datum.[/]")));
                    Messages.WaitForKey();
                    continue;
                }
                DateTime end = CalendarMenu.CalendarController("Utceckningsdatum");                
                
                DateOnly endDate = DateOnly.FromDateTime(end);

                
                if (endDate < startDate)
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Slutdatum kan inte vara före startdatum.[/]")));
                    Messages.WaitForKey();
                    continue;
                }
                Console.Clear();
                var numberOfGuests = VarValidater.GetRequiredIntOverZero("Ange antal personer som ska dela rummet: ");
                if (numberOfGuests <= 0)
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Antal gäster måste vara minst 1.[/]")));
                    Messages.WaitForKey();
                    continue;
                }

                return (startDate, endDate, numberOfGuests);
            }
        }
    }
}
