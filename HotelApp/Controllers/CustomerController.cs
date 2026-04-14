using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class CustomerController(CustomerService service)
    {
        public void Create()
        {
            Console.Clear();
            AnsiConsole.Write(Align.Center(new Markup("[cyan]Skapa en ny gäst[/]")));

            var firstNameInput = VarValidater.GetRequiredStringMax25("Ange förnamn: ");
            var lastNameInput = VarValidater.GetRequiredStringMax25("Ange efternamn: ");
            var emailInput = VarValidater.GetRequiredEmail("Ange email: ");
            var birthdayInput = VarValidater.GetValidDateOnly("Ange födelseår (yyyy-MM-dd): ");

            if (service.CustomerExists(firstNameInput, lastNameInput, emailInput))
            {
                AnsiConsole.MarkupLine("[red]Gästen finns redan. Alternativt E-postadressen används redan.[/]");
                return;
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - birthdayInput.Year;

            Console.Clear();
            AnsiConsole.MarkupLine("\n[bold green]Sammanfattning:[/]");

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

            service.CreateCustomer(firstNameInput, lastNameInput, birthdayInput, emailInput);

            AnsiConsole.MarkupLine("[bold green]Kund registrerad framgångsrikt![/]");
        }
        public void Delete()
        {
            Console.Clear();

            var activeCustomers = service.GetActiveCustomers();

            if (!activeCustomers.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga gäster att radera.[/]");
                return;
            }

            var options = activeCustomers.Select(c => $"{c.FullName} {c.Age()} år").ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);

            var customerToDelete = activeCustomers[index];

            if (service.HasActiveBookings(customerToDelete))
            {
                AnsiConsole.MarkupLine("[red]Har aktiva bokningar.[/]");
                return;
            }

            service.Delete(customerToDelete);

            AnsiConsole.MarkupLine("[red]Gäst borttagen.[/]");
            Messages.WaitForKey();
        }
        public void Update()
        {
            Console.Clear();

            var customers = service.GetActiveCustomers();

            if (!customers.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga gäster att uppdatera.[/]");
                return;
            }

            var options = customers.Select(c => c.FullName).ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);

            var customer = customers[index];

            var updateOptions = new List<string> { "Förnamn", "Efternamn", "Födelsedatum", "Email" };
            int choice = ScrollMenu.ShowScrollingMenu(updateOptions);

            switch (choice)
            {
                case 0:
                    customer.FirstName = VarValidater.GetRequiredStringMax25("Nytt förnamn: ");
                    break;

                case 1:
                    customer.LastName = VarValidater.GetRequiredStringMax25("Nytt efternamn: ");
                    break;

                case 2:
                    customer.Birthday = VarValidater.GetValidDateOnly("Nytt datum: ");
                    break;
                case 3:
                    customer.Email = VarValidater.GetRequiredEmail("Ny email: ");
                    break;
            }

            service.Update(customer);

            AnsiConsole.MarkupLine("[green]Uppdaterad![/]");
            Messages.WaitForKey();
        }
        public void Read()
        {
            Console.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var activeGuests = service.GetActiveCustomers();
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
        public void Reactivate()
        {
            Console.Clear();

            var inactiveCustomers = service.GetInactiveCustomers();

            if (!inactiveCustomers.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga att återaktivera.[/]");
                return;
            }

            var options = inactiveCustomers.Select(c => c.FullName).ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);

            var customer = inactiveCustomers[index];

            service.Reactivate(customer);

            AnsiConsole.MarkupLine("[green]Återaktiverad![/]");
            Messages.WaitForKey();
        }
        public void ReadDeleted()
        {
            Console.Clear();
            var inactiveGuests = service.GetInactiveCustomers();
            if (!inactiveGuests.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga inaktiva gäster.[/]");
                return;
            }
            foreach (var guest in inactiveGuests)
            {
                var content = new Markup($"[yellow]Namn:[/] {guest.FullName}" +
                    $"\n [blue]Ålder:[/] {guest.Age()}" +
                    $"\n ====================");

                AnsiConsole.Write(Align.Center(content));
            }


            Messages.WaitForKey();
            Console.Clear();

        }

    }
}
