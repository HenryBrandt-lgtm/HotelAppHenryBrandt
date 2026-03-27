using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.Services.CreateBookingServices;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HotelApp.Services
{
    public class BookingService : ICrud
    {
        public void Create()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            BookingDate bookingConditions = new BookingDate();
            AvailableRooms available = new AvailableRooms();
            CustomerSelection customerSelection = new CustomerSelection();
            CustomerService customerService = new CustomerService();

            using (var dbContext = new ApplicationDbContext())
            {
                // 1. välj datum och antal gäster
                var result = bookingConditions.SetVisitingConditions();
                var startDate = result.StartDate;
                var endDate = result.EndDate;
                int numberOfGuests = result.NumberOfGuests;

                // 2️. Filtrera fram lediga rum under perioden                
                var availableRooms = available.SetAvailableRooms(startDate, endDate, numberOfGuests);

                if (availableRooms == null || availableRooms.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]Inga tillräckligt stora rum lediga.[/]");
                    Messages.WaitForKey();
                    return;
                }
                // 3️. Visa lediga rum och välj
                var selectedRoom = available.SelectRoom(availableRooms, startDate, endDate);

                // 4️. välj/skapa kund                
                var selectedCustomer = customerSelection.SelectOrCreateCustomer(customerService);

                // 6. Skapa bokning
                var newBooking = new Booking
                {
                    CustomerId = selectedCustomer.CustomerId,
                    RoomId = selectedRoom.RoomId,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                dbContext.Booking.Add(newBooking);
                dbContext.SaveChanges();
                //7. Ge totalpris
                decimal totalCost = selectedRoom.GetTotalPrice(startDate, endDate);
                AnsiConsole.MarkupLine($"[green]Bokningen skapad! Total kostnad: {totalCost:C}[/]");
            }

            Messages.WaitForKey();

        }

        public void Delete()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.Clear();
                var selectMessage = new Text($"Välj från listan med bokningar vilken du vill ta bort.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(selectMessage);
                Messages.WaitForKey();

                var activeBookings = dbContext.Booking.Include(b => b.Customer).Include(b => b.Room).ToList().Where(f => f.IsActive).ToList();
                if (!activeBookings.Any())
                {
                    AnsiConsole.MarkupLine("[red]Inga bokningar att radera.[/]");
                    return;
                }

                var bookingOptions = activeBookings.Select(f => $"ID: {f.BookingId} Namn: {f.Customer.FullName} " +
                $"Incheckning: {f.StartDate} Utcheckning: {f.EndDate}").ToList();

                int index = ScrollMenu.ScrollingMenu(bookingOptions);

                var bookingToRemove = activeBookings[index];

                var confirmMessage = new Text($"Är du säker på att du vill ta bort bokning ID: {bookingToRemove.BookingId}? (J/N)", new Style(Color.Yellow))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(confirmMessage);

                var key = Console.ReadKey(true).Key;
                if (key != ConsoleKey.J)
                {
                    AnsiConsole.MarkupLine("[green]Borttagning avbruten.[/]");
                    return;
                }
                dbContext.Booking.Remove(bookingToRemove);
                dbContext.SaveChanges();

                var removedbooking = new Text($"Bokning med ID: {bookingToRemove.BookingId} har blivit raderad.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(removedbooking);
                AnsiConsole.WriteLine();
                Messages.WaitForKey();
                Console.Clear();

            }
        }

        public void Reactivate()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            using (var dbContext = new ApplicationDbContext())
            {
                var activeBookings = dbContext.Booking.Include(b => b.Customer).Include(b => b.Room).ToList()
                    .Where(b => b.IsActive).ToList();

                if (!activeBookings.Any())
                {
                    AnsiConsole.WriteLine("[red]Inga aktiva bokningar att visa.[/]");
                    return;
                }

                var panels = activeBookings.Select(booking =>
                    new Panel(
                        $"[yellow]Kund:[/] {booking.Customer.FullName}  " +
                        $"[blue]Rum:[/] {booking.Room.RoomName}  " +
                        $"[yellow]StartDatum:[/] {booking.StartDate}  " +
                        $"[blue]SlutDatum:[/] {booking.EndDate}  " +
                        $"[yellow]Nätter:[/] {booking.DurationDays}  " +
                        $"[blue]Totalkostnad:[/] {booking.TotalCost:C}  " +
                        $"[green]Status:[/] {(booking.IsBooked() ? "[red]Pågående[/]" : "Ej pågående")}"
                    )
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Expand()
                ).ToList();

                AnsiConsole.Write(new Columns(panels));
            }
            Messages.WaitForKey();
        }

        public void ReadDeleted()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med boknignar vilken du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            Messages.WaitForKey();

            using (var dbContext = new ApplicationDbContext())
            {
                var activeBookings = dbContext.Booking.Include(b => b.Customer).Include(b => b.Room).ToList().Where(f => f.IsActive).ToList();
                if (!activeBookings.Any())
                {
                    AnsiConsole.MarkupLine("[red]Inga bokningar att radera.[/]");
                    return;
                }
                var bookingOptions = activeBookings.Select(f => $"ID: {f.BookingId} Namn: {f.Customer.FullName} " +
                $"Incheckning: {f.StartDate} Utcheckning: {f.EndDate}").ToList();

                int index = ScrollMenu.ScrollingMenu(bookingOptions);

                var bookingToUpdate = activeBookings[index];
                var updateOptions = new List<string> { "Incheckningsdatum", "Utcheckningsdatum", "Rum" };

                int updateIndex = ScrollMenu.ScrollingMenu(updateOptions);

                switch (updateIndex)
                {
                    case 0:
                        var newStart = VarValidater.GetValidBookingDate("Nytt incheckningsdatum: ");
                        if (!bookingToUpdate.Room.IsAvailable(newStart, bookingToUpdate.EndDate, bookingToUpdate.BookingId))
                        {
                            AnsiConsole.MarkupLine("[red]Rummet är redan bokat under den perioden![/]");
                            return;
                        }
                        bookingToUpdate.StartDate = newStart;

                        if (bookingToUpdate.EndDate < bookingToUpdate.StartDate)
                        {
                            AnsiConsole.MarkupLine("[red]Startdatum kan inte vara efter slutdatum.[/]");
                            return;
                        }
                        break;

                    case 1:
                        var newEnd = VarValidater.GetValidBookingDate("Nytt utcheckningsdatum: ");
                        if (!bookingToUpdate.Room.IsAvailable(bookingToUpdate.StartDate, newEnd, bookingToUpdate.BookingId))
                        {
                            AnsiConsole.MarkupLine("[red]Rummet är redan bokat under den perioden![/]");
                            return;
                        }
                        bookingToUpdate.EndDate = newEnd;

                        if (bookingToUpdate.EndDate < bookingToUpdate.StartDate)
                        {
                            AnsiConsole.MarkupLine("[red]Slutdatum kan inte vara före startdatum.[/]");
                            return;
                        }
                        break;

                    case 2:
                        var availableRooms = dbContext.Room.Include(r => r.Bookings).Where(r => r.IsActive).ToList()
                            .Where(r => r.IsAvailable(bookingToUpdate.StartDate, bookingToUpdate.EndDate, bookingToUpdate.BookingId)).ToList();

                        if (availableRooms.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]Inga rum lediga under de valda datumen.[/]");
                            return;
                        }

                        var roomPanels = availableRooms.Select(r =>
                        {
                            var totalCost = r.GetTotalPrice(bookingToUpdate.StartDate, bookingToUpdate.EndDate);

                            return new Panel($"[yellow]Rum:[/] {r.RoomName}  [blue]Typ:[/] {r.RoomType}  " +
                                $"[yellow]Pris/natt:[/] {r.Price:C} [blue]TotalPris:[/] {totalCost}")
                                .Border(BoxBorder.Rounded)
                                .Padding(1, 1)
                                .Header($"{r.RoomId}: {r.RoomName}")
                                .Expand();
                        }).ToList();

                        AnsiConsole.MarkupLine("[bold]Välj rum:[/]");
                        AnsiConsole.Write(new Columns(roomPanels));

                        int selectedRoomId = VarValidater.GetRequiredInt("Ange rummets ID i listan: ");
                        var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == selectedRoomId);
                        if (selectedRoom == null)
                        {
                            AnsiConsole.MarkupLine("[red]Ogiltigt ID försök igen.[/]");
                            return;
                        }
                        bookingToUpdate.Room = selectedRoom;
                        break;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
