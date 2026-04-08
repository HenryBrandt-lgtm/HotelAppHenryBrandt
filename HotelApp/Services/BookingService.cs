using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using HotelApp.Services.CreateBookingServices;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HotelApp.Services
{
    public class BookingService 
    {
        private readonly ApplicationDbContext _db;

        public BookingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            BookingDate bookingConditions = new BookingDate();
            AvailableRooms available = new AvailableRooms(_db);
            CustomerSelection customerSelection = new CustomerSelection(_db);
            CustomerService customerService = new CustomerService(_db);


            // 1. välj datum och antal gäster
            var result = bookingConditions.SetVisitingConditions();
            var startDate = result.StartDate;
            var endDate = result.EndDate;
            int numberOfGuests = result.NumberOfGuests;

            // 2️. Filtrera fram lediga rum under perioden                
            var availableRooms = available.SetAvailableRooms(startDate, endDate, numberOfGuests);

            if (availableRooms == null || availableRooms.Count == 0)
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga tillräckligt stora rum lediga.[/]")));
                Messages.WaitForKey();
                return;
            }
            // 3️. Visa lediga rum och välj
            var selectedRoom = available.SelectRoom(availableRooms, startDate, endDate);

            // 4️. välj/skapa kund                
            var selectedCustomer = customerSelection.SelectOrCreateCustomer(customerService);

            // 6. Skapa bokning
            decimal totalCost = selectedRoom.GetTotalPrice(startDate, endDate);

            var newBooking = new Booking
            {
                CustomerId = selectedCustomer.CustomerId,
                RoomId = selectedRoom.RoomId,
                StartDate = startDate,
                EndDate = endDate,
                Invoice = new Invoice
                {
                    Amount = totalCost,
                    InvoiceDate = DateTime.Now,
                    IsPaid = false
                }
            };

            _db.Bookings.Add(newBooking);
            _db.SaveChanges();
            //7. Ge totalpris
            AnsiConsole.Write(Align.Center(new Markup($"[green]Bokningen skapad! Total kostnad: {totalCost:C}[/]")));


            Messages.WaitForKey();

        }

        public void Delete()
        {

            Console.Clear();
            var selectMessage = new Text($"Välj från listan med bokningar vilken du vill ta bort.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            Messages.WaitForKey();

            var activeBookings = _db.Bookings.Include(b => b.Customer).Include(b => b.Room).Include(b => b.Invoice).ToList().Where(f => f.IsActive).ToList();
            if (!activeBookings.Any())
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga bokningar att radera.[/]")));
                return;
            }

            var bookingOptions = activeBookings.Select(f => $"ID: {f.BookingId} Namn: {f.Customer.FullName} " +
            $"Incheckning: {f.StartDate} Utcheckning: {f.EndDate} Betalning: {(f.Invoice.IsPaid ? "Betald" : "Ej betald")}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(bookingOptions);

            var bookingToRemove = activeBookings[index];

            var confirmMessage = new Text($"Är du säker på att du vill ta bort bokning ID: {bookingToRemove.BookingId}? (J/N)", new Style(Color.Yellow))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(confirmMessage);

            var key = Console.ReadKey(true).Key;
            if (key != ConsoleKey.J)
            {
                AnsiConsole.Write(Align.Center(new Markup("[green]Borttagning avbruten.[/]")));
                return;
            }
            _db.Bookings.Remove(bookingToRemove);
            _db.SaveChanges();
            if (bookingToRemove.Invoice.IsPaid)
            {
                AnsiConsole.Write(Align.Center(new Markup($"Betalningen för bokningen har blivit återbetald och\n" +
                    $"[red]Bokning med ID: {bookingToRemove.BookingId} har blivit raderad.[/]")));               
            }
            AnsiConsole.Write(Align.Center(new Markup($"[red]Bokning med ID: {bookingToRemove.BookingId} har blivit raderad.[/]")));
            AnsiConsole.WriteLine();
            Messages.WaitForKey();
            Console.Clear();


        }

        public void Reactivate()
        {
            throw new NotImplementedException();
        }

        public void Pay()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();


            var unpaidBookings = _db.Bookings.Include(b => b.Customer).Include(b => b.Room).Include(b => b.Invoice).ToList()
                .Where(b => b.IsActive && b.Invoice != null && !b.Invoice.IsPaid).ToList();

            if (!unpaidBookings.Any())
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga obetalda bokningar att visa.[/]")));
                Messages.WaitForKey();
                return;
            }
            var option = ScrollMenu.ShowScrollingMenu(unpaidBookings.Select(b => $"Boknings ID: {b.BookingId} " +
            $"Kund: {b.Customer.FullName} Incheckningsdag: {b.StartDate} Belopp {b.Invoice.Amount}kr)").ToList());

            var selectedBooking = unpaidBookings.ElementAt(option);

            selectedBooking.Invoice.IsPaid = true;

            _db.SaveChanges();

            AnsiConsole.Write(Align.Center(new Markup("[green]Fakturan är nu betald![/]")));
            Messages.WaitForKey();
        }

        public void Read()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();


            var activeBookings = _db.Bookings.Include(b => b.Customer).Include(b => b.Room).Include(b => b.Invoice).ToList()
                .Where(b => b.IsActive).ToList();

            if (!activeBookings.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga aktiva bokningar att visa.[/]");
                Messages.WaitForKey();
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
                    $"[green]Status:[/] {(booking.Invoice.IsPaid ? "Betald" : "[red]Ej betald[/]")}  " +
                    $"[green]Status:[/] {(booking.IsBooked() ? "[red]Pågående[/]" : "Ej pågående")}"
                )
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand()
            ).ToList();

            AnsiConsole.Write(new Columns(panels));
            Messages.WaitForKey();
        }
        public void ReadAvailableRooms()
        {
            BookingDate bookingConditions = new BookingDate();
            AvailableRooms available = new AvailableRooms(_db);


            // 1. välj datum och antal gäster
            var result = bookingConditions.SetVisitingConditions();
            var startDate = result.StartDate;
            var endDate = result.EndDate;
            int numberOfGuests = result.NumberOfGuests;

            // 2️. Filtrera fram lediga rum under perioden                
            var availableRooms = available.SetAvailableRooms(startDate, endDate, numberOfGuests);

            if (availableRooms == null || availableRooms.Count == 0)
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga tillräckligt stora rum lediga.[/]")));
                Messages.WaitForKey();
                return;
            }
            // 3️. Visa lediga rum och välj
            available.ReadAvailableRooms(availableRooms, startDate, endDate);
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


            var activeBookings = _db.Bookings.Include(b => b.Customer).Include(b => b.Room).ToList().Where(f => f.IsActive).ToList();
            if (!activeBookings.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga bokningar att radera.[/]");
                return;
            }
            var bookingOptions = activeBookings.Select(f => $"ID: {f.BookingId} Namn: {f.Customer.FullName} " +
            $"Incheckning: {f.StartDate} Utcheckning: {f.EndDate}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(bookingOptions);

            var bookingToUpdate = activeBookings[index];
            var updateOptions = new List<string> { "Incheckningsdatum", "Utcheckningsdatum", "Rum" };

            int updateIndex = ScrollMenu.ShowScrollingMenu(updateOptions);

            switch (updateIndex)
            {
                case 0:
                    var newStart = VarValidater.GetValidBookingDate("Nytt incheckningsdatum: ");
                    if (!bookingToUpdate.Room.IsAvailable(newStart, bookingToUpdate.EndDate, bookingToUpdate.BookingId))
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Rummet är redan bokat under den perioden![/]")));
                        return;
                    }
                    bookingToUpdate.StartDate = newStart;

                    if (bookingToUpdate.EndDate < bookingToUpdate.StartDate)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Startdatum kan inte vara efter slutdatum.[/]")));
                        return;
                    }
                    break;

                case 1:
                    var newEnd = VarValidater.GetValidBookingDate("Nytt utcheckningsdatum: ");
                    if (!bookingToUpdate.Room.IsAvailable(bookingToUpdate.StartDate, newEnd, bookingToUpdate.BookingId))
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Rummet är redan bokat under den perioden![/]")));
                        return;
                    }
                    bookingToUpdate.EndDate = newEnd;

                    if (bookingToUpdate.EndDate < bookingToUpdate.StartDate)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Slutdatum kan inte vara före startdatum.[/]")));
                        return;
                    }
                    break;

                case 2:
                    var availableRooms = _db.Rooms.Include(r => r.Bookings).Where(r => r.IsActive).ToList()
                        .Where(r => r.IsAvailable(bookingToUpdate.StartDate, bookingToUpdate.EndDate, bookingToUpdate.BookingId)).ToList();

                    if (availableRooms.Count == 0)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Inga rum lediga under de valda datumen.[/]")));
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

                    AnsiConsole.Write(Align.Center(new Markup("[bold]Välj rum:[/]")));
                    AnsiConsole.Write(new Columns(roomPanels));

                    int selectedRoomId = VarValidater.GetRequiredIntOverZero("Ange rummets ID i listan: ");
                    var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == selectedRoomId);
                    if (selectedRoom == null)
                    {
                        AnsiConsole.Write(Align.Center(new Markup("[red]Ogiltigt ID försök igen.[/]")));
                        return;
                    }
                    bookingToUpdate.Room = selectedRoom;
                    break;
            }

            _db.SaveChanges();

        }

    }
}