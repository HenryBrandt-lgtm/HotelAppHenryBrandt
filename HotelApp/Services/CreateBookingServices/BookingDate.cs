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
                var startMessage = "Inceckningsdatum";
                var endMessage = "Utceckningsdatum";
                DateTime start = CalendarMenu.CalendarController(startMessage);

                DateTime end = CalendarMenu.CalendarController(endMessage);                

                DateOnly startDate = DateOnly.FromDateTime(start);
                DateOnly endDate = DateOnly.FromDateTime(end);

                if (endDate < startDate)
                {
                    AnsiConsole.MarkupLine("[red]Slutdatum kan inte vara före startdatum.[/]");
                    continue;
                }
                var numberOfGuests = VarValidater.GetRequiredInt("Ange antal personer som ska dela rummet: ");
                if (numberOfGuests <= 0)
                {
                    AnsiConsole.MarkupLine("[red]Antal gäster måste vara minst 1.[/]");
                    continue;
                }

                return (startDate, endDate, numberOfGuests);
            }

        }
    }
}
