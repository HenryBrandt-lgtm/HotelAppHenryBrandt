using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using Spectre.Console;

namespace HotelApp.Services.CreateBookingServices
{
    public class CustomerSelection
    {
        public Customer SelectOrCreateCustomer(CustomerService customerService)
        {
            using var dbContext = new ApplicationDbContext();
            {
                while (true)
                {
                    Console.Clear();
                    var activeCustomers = dbContext.Customer.Where(c => c.IsActive).ToList();

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
                        customerService.Create();
                        continue;
                    }

                    var selectedCustomer = activeCustomers.ElementAtOrDefault(customerIndex);
                    if (selectedCustomer != null)
                        return selectedCustomer;

                    AnsiConsole.MarkupLine("[red]Ogiltigt val, försök igen.[/]");
                }
            }
        }
    }
}

