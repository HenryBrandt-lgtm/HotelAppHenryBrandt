using HotelApp.Controllers;
using HotelApp.Data;
using Spectre.Console;

namespace HotelApp.Services.CreateBookingServices
{
    public class BookingDate
    {
        public (DateOnly StartDate, DateOnly EndDate, int NumberOfGuests) SetVisitingConditions()
        {

            while (true)
            {
                DateOnly startDate = VarValidater.GetValidBookingDate("Ange startdatum (yyyy-MM-dd): ");
                DateOnly endDate = VarValidater.GetValidBookingDate("Ange slutdatum (yyyy-MM-dd): ");

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
