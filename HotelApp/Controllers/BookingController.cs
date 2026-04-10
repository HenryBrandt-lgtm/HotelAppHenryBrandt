using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class BookingController(BookingService bookingService, CustomerService customerService, CustomerController customerController)
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


                if (endDate <= startDate)
                {
                    AnsiConsole.Write(Align.Center(new Markup("[red]Slutdatum kan inte vara före eller samam dag som startdatum.[/]")));
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
        public void ReadAvailableRooms(List<Room> availableRooms, DateOnly startDate, DateOnly endDate)
        {
            Console.Clear();
            var roomPanels = availableRooms.Select(r =>
            {
                var totalCost = r.GetTotalPrice(startDate, endDate);

                return new Panel(
                    $"[yellow]Rum:[/] {r.RoomName}  [blue]Typ:[/] {r.RoomType}  " +
                    $"[yellow]Storlek:[/] {r.Area}kvm " +
                    $"[blue]Pris/natt:[/] {r.Price:C} [yellow]TotalPris:[/] {totalCost} " +
                    $"[blue]Antal sängar:[/] {r.Beds} [yellow]Antal extrasängar:[/] {r.ExtraBeds}")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header($"{r.RoomId}: {r.RoomName}")
                    .Expand();
            }).ToList();
            AnsiConsole.MarkupLine("[bold]Lediga rum:[/]");
            AnsiConsole.Write(new Columns(roomPanels));
        }
        public Room SelectRoom(List<Room> availableRooms, DateOnly startDate, DateOnly endDate)
        {
            ReadAvailableRooms(availableRooms, startDate, endDate);
            Room selectedRoom = null;
            while (true)
            {
                int selectedRoomId = VarValidater.GetRequiredIntOverZero("Ange rummets ID i listan: ");

                selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == selectedRoomId);

                if (selectedRoom != null)
                {
                    return selectedRoom;
                }
                AnsiConsole.MarkupLine("[red]Ogiltigt ID försök igen.[/]");
            }
        }
        public void Create()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();


            // 1. välj datum och antal gäster
            var result = SetVisitingConditions();
            var startDate = result.StartDate;
            var endDate = result.EndDate;
            int numberOfGuests = result.NumberOfGuests;

            // 2️. Filtrera fram lediga rum under perioden                
            var availableRooms = bookingService.SetAvailableRooms(startDate, endDate, numberOfGuests);

            if (availableRooms == null || availableRooms.Count == 0)
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga tillräckligt stora rum lediga.[/]")));
                Messages.WaitForKey();
                return;
            }
            // 3️. Visa lediga rum och välj
            var selectedRoom = SelectRoom(availableRooms, startDate, endDate);

            // 4️. välj/skapa kund                
            var selectedCustomer = SelectOrCreateCustomer();

            // 6. Skapa bokning
            decimal totalCost = selectedRoom.GetTotalPrice(startDate, endDate);

            bookingService.Create(selectedCustomer, selectedRoom, startDate, endDate, totalCost);

            AnsiConsole.Write(Align.Center(new Markup($"[green]Bokningen skapad! Total kostnad: {totalCost:C}[/]")));


            Messages.WaitForKey();

        }
        public Customer SelectOrCreateCustomer()
        {

            while (true)
            {
                Console.Clear();
                var activeCustomers = customerService.GetActiveCustomers();

                var customerPanels = activeCustomers.Select((c, idx) =>
                    new Panel($"[yellow]Namn:[/] {c.FullName}  [blue]Ålder:[/] {c.Age()}")
                        .Border(BoxBorder.Rounded)
                        .Padding(1, 1)
                        .Header($"{idx}: {c.LastName}")
                        .Expand()
                ).ToList();

                var createNewPanel = new Panel("[green]Skapa ny kund[/]")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header($"{activeCustomers.Count}: Ny kund")
                    .Expand();

                customerPanels.Add(createNewPanel);

                AnsiConsole.MarkupLine("[bold]Välj kund:[/]");
                AnsiConsole.Write(new Columns(customerPanels));

                int customerIndex = VarValidater.GetRequiredInt("Ange kundens nummer i listan: ");

                if (customerIndex == activeCustomers.Count)
                {
                    customerController.Create();
                    continue;
                }

                var selectedCustomer = activeCustomers.ElementAtOrDefault(customerIndex);
                if (selectedCustomer != null)
                    return selectedCustomer;

                AnsiConsole.MarkupLine("[red]Ogiltigt val, försök igen.[/]");
            }
        }
        public void ReadAvailableRooms()
        {

            var result = SetVisitingConditions();
            var startDate = result.StartDate;
            var endDate = result.EndDate;
            int numberOfGuests = result.NumberOfGuests;

            var availableRooms = bookingService.SetAvailableRooms(startDate, endDate, numberOfGuests);

            if (availableRooms == null || availableRooms.Count == 0)
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga tillräckligt stora rum lediga.[/]")));
                Messages.WaitForKey();
                return;
            }
            ReadAvailableRooms(availableRooms, startDate, endDate);
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

            var bookings = bookingService.GetAllBookings();
            if (!bookings.Any())
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga bokningar att radera.[/]")));
                return;
            }

            var bookingOptions = bookings.Select(f => $"ID: {f.BookingId} Namn: {f.Customer.FullName} " +
            $"Incheckning: {f.StartDate} Utcheckning: {f.EndDate} Betalning: {(f.Invoice.IsPaid ? "Betald" : "Ej betald")}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(bookingOptions);

            var bookingToRemove = bookings[index];

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

            if (bookingToRemove.Invoice.IsPaid)
            {
                bookingService.Delete(bookingToRemove);
                AnsiConsole.Write(Align.Center(new Markup($"Betalningen för bokningen har blivit återbetald och\n" +
                    $"[red]Bokning med ID: {bookingToRemove.BookingId} har blivit raderad.[/]")));
            }
            else
            {
                bookingService.Delete(bookingToRemove);
                AnsiConsole.Write(Align.Center(new Markup($"[red]Bokning med ID: {bookingToRemove.BookingId} har blivit raderad.[/]")));
            }
            AnsiConsole.WriteLine();
            Messages.WaitForKey();
            Console.Clear();
        }
        public void Pay()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();


            var unpaidBookings = bookingService.GetUnpaidBookings();

            if (!unpaidBookings.Any())
            {
                AnsiConsole.Write(Align.Center(new Markup("[red]Inga obetalda bokningar att visa.[/]")));
                Messages.WaitForKey();
                return;
            }
            var option = ScrollMenu.ShowScrollingMenu(unpaidBookings.Select(b => $"Boknings ID: {b.BookingId} " +
            $"Kund: {b.Customer.FullName} Incheckningsdag: {b.StartDate} Belopp {b.Invoice.Amount}kr)").ToList());

            var bookingToPay = unpaidBookings.ElementAt(option);

            bookingService.Pay(bookingToPay);

            AnsiConsole.Write(Align.Center(new Markup("[green]Fakturan är nu betald![/]")));
            Messages.WaitForKey();
        }
        public void Read()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();


            var activeBookings = bookingService.GetActiveBookings();

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
        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med boknignar vilken du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            Messages.WaitForKey();


            var activeBookings = bookingService.GetActiveBookings();
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
                    var availableRooms = bookingService
                        .GetAvailableRoomsForBooking(bookingToUpdate.StartDate, bookingToUpdate.EndDate, bookingToUpdate.BookingId);

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
            bookingService.Update(bookingToUpdate);
        }
    }
}
