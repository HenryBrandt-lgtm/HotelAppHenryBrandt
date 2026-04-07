using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class CustomerService : ICrud
    {
        private readonly ApplicationDbContext _db;
        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Read()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var activeGuests = _db.Customers.Where(c => c.IsActive == true);
            if (!activeGuests.Any())
            {
                AnsiConsole.WriteLine("Inga aktiva gäster att visa.");
                return;
            }
            else
            {
                var panels = activeGuests.Select(guest =>
                    new Panel(
                        $"[yellow]Namn:[/] {guest.FullName}" +
                        $"\n [blue]Ålder:[/] {guest.Age()}"
                        )
                    .Border(BoxBorder.Rounded)
                    .Header($"[bold]{guest.LastName}[/]")
                    .Padding(1, 1)
                    .Expand()
                    ).ToList();

                var columns = new Columns(panels);
                AnsiConsole.Write(columns);
            }



            Messages.WaitForKey();

        }
        public void Delete()
        {

            Console.Clear();
            var selectMessage = new Text($"Välj från listan med gäster vem du vill ta bort.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            Messages.WaitForKey();

            var activeCustomers = _db.Customers.Where(c => c.IsActive).ToList();
            if (!activeCustomers.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga gäster att radera.[/]");
                return;
            }

            var nameOptions = activeCustomers.Select(c => $"{c.FullName} {c.Age()} år").ToList();

            int index = ScrollMenu.ShowScrollingMenu(nameOptions);

            var customerToRemove = activeCustomers[index];

            if (customerToRemove.Bookings?.Any(b => b.IsActive) == true)
            {
                AnsiConsole.MarkupLine("[red]Gästen har aktiva bokningar och kan inte tas bort.[/]");
                Messages.WaitForKey();
                return;
            }

            customerToRemove.IsActive = false;
            _db.SaveChanges();


            var removedCustomer = new Text($"{customerToRemove.FullName} har tagits bort.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(removedCustomer);
            Messages.WaitForKey();

            Console.Clear();
        }


        public void Create()
        {

            Console.Clear();
            AnsiConsole.Write(Align.Center(new Markup("[cyan]Skapa en ny gäst[/]")));

            var firstNameInput = VarValidater.GetRequiredString("Ange förnamn: ");

            var lastNameInput = VarValidater.GetRequiredString("Ange efternamn: ");

            var ageInput = VarValidater.GetValidDateOnly("Ange födelseår (yyy-MM-dd): ");

            if (_db.Customers.Any(c => c.FirstName == firstNameInput && c.LastName == lastNameInput))
                Console.WriteLine("Gästen finns redan inlagd. Dubbelkika bland raderade gäster om hen är inaktiverad");
            else
            {
                var timeNow = DateOnly.FromDateTime(DateTime.Today);
                int age = timeNow.Year - ageInput.Year;
                Console.Clear();
                AnsiConsole.MarkupLine("\n[bold green]Sammanfattning av kundinformation:[/]");
                var table = new Table();
                table.AddColumn("[red]Fält[/]");
                table.AddColumn("[red]Värde[/]");
                table.AddRow("Förnamn", firstNameInput);
                table.AddRow("Efternamn", lastNameInput);
                table.AddRow("Ålder", age.ToString());
                AnsiConsole.Write(table);

                AnsiConsole.Write("\nÄr alla uppgifter korrekta? (J/N)");

                var key = Console.ReadKey(true).Key;
                if (key != ConsoleKey.J)
                {
                    AnsiConsole.MarkupLine("[red]Avbryter.[/]");
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold green]Kund registrerad framgångsrikt![/]");
                    _db.Customers.Add(new Customer
                    {
                        FirstName = firstNameInput,
                        LastName = lastNameInput,
                        Birthday = ageInput,
                        IsActive = true
                    });
                    _db.SaveChanges();
                }
            }
        }


        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med gäster vem du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);

            Messages.WaitForKey();

            var activeCustomers = _db.Customers.Where(c => c.IsActive).ToList();
            if (!activeCustomers.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga gäster att uppdatera.[/]");
                return;
            }

            var nameOptions = activeCustomers.Select(c => $"Namn: {c.FullName} person.nr: {c.Birthday}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(nameOptions);

            var customerToUpdate = activeCustomers[index];
            var updateOptions = new List<string> { "FirstName", "LastName", "Birthday" };

            int updateIndex = ScrollMenu.ShowScrollingMenu(updateOptions);
            var header = HeaderDisplay.GetHeader();

            switch (updateIndex)
            {
                case 0:
                    customerToUpdate.FirstName = VarValidater.GetRequiredString($"Tidigare namn: {customerToUpdate.FirstName}" +
                        $"\nNytt förnamn: ");
                    Console.Clear();
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(header);
                    AnsiConsole.Write(Align.Center(new Panel($"[yellow]Förnamnet har uppdaterats till[/][cyan] {customerToUpdate.FirstName}[/]")
                        .Border(BoxBorder.Rounded)));
                    Messages.WaitForKey();
                    break;

                case 1:
                    customerToUpdate.LastName = VarValidater.GetRequiredString($"Tidigare namn: {customerToUpdate.LastName}" +
                        $"\nNytt efternamn: ");
                    Console.Clear();
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(header);
                    AnsiConsole.Write(Align.Center(new Panel($"[yellow]Efternamnet har uppdaterats till[/][cyan] {customerToUpdate.LastName}[/]")
                        .Border(BoxBorder.Rounded)));

                    Messages.WaitForKey();
                    break;

                case 2:
                    customerToUpdate.Birthday = VarValidater.GetValidDateOnly($"Tidigare person.nr: {customerToUpdate.Birthday}" +
                        $"\nNytt person.nr(yyyy-MM-dd): ");
                    Console.Clear();
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(header);
                    AnsiConsole.Write(Align.Center(new Panel($"[yellow]Person.nr har uppdaterats till[/][cyan] {customerToUpdate.Birthday}[/]")
                        .Border(BoxBorder.Rounded)));

                    Messages.WaitForKey();
                    break;
            }

            _db.SaveChanges();
        }


        public void ReadDeleted()
        {

            foreach (var guest in _db.Customers.Where(c => c.IsActive == false))
            {
                var content = new Markup($"[yellow]Namn:[/] {guest.FullName}" +
                    $"\n [blue]Ålder:[/] {guest.Age()}" +
                    $"\n ====================");

                AnsiConsole.Write(Align.Center(content));
            }


            Messages.WaitForKey();
            Console.Clear();

        }
        public void Reactivate()
        {

            Console.Clear();
            var selectMessage = new Text($"Välj från listan med gäster vem du vill ta tillbaka.", new Style(Color.Cyan))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            Messages.WaitForKey();

            var activeCustomers = _db.Customers.Where(c => !c.IsActive).ToList();
            if (!activeCustomers.Any())
            {
                AnsiConsole.MarkupLine("[red]Det finns inga inaktiva gäster.[/]");
                return;
            }

            var nameOptions = activeCustomers.Select(c => $"{c.FullName} {c.Age()} år").ToList();

            int index = ScrollMenu.ShowScrollingMenu(nameOptions);

            var returningCustomer = activeCustomers[index];
            returningCustomer.IsActive = true;
            _db.SaveChanges();


            var reactivatedCustomer = new Text($"{returningCustomer.FullName} har återaktiverats.", new Style(Color.Green))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(reactivatedCustomer);
            Messages.WaitForKey();

            Console.Clear();
        }
    }
}

