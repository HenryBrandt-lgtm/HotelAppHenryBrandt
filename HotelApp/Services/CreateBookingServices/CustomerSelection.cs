using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using Spectre.Console;

namespace HotelApp.Services.CreateBookingServices
{
    public class CustomerSelection
    {

        public List<Customer> SetAvailableCustomers()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var activeCustomers = dbContext.Customer.Where(c => c.IsActive).ToList();


                var customerPanels = activeCustomers.Select((c, idx) =>
                    new Panel($"[yellow]Namn:[/] {c.FullName}  [blue]Ålder:[/] {c.Age()}")
                        .Border(BoxBorder.Rounded)
                        .Padding(1, 1)
                        .Header($"{idx}: {c.LastName}")
                        .Expand()
                ).ToList();
                AnsiConsole.MarkupLine("[bold]Välj kund:[/]");
                AnsiConsole.Write(new Columns(customerPanels));
                return activeCustomers;
            }
        }
        public Customer SelectCustomer(List<Customer> activeCustomers)
        {

            while (true)
            {
                int customerIndex = VarValidater.GetRequiredInt("Ange kundens nummer i listan: ");

                var selectedCustomer = activeCustomers.ElementAtOrDefault(customerIndex);

                if (selectedCustomer != null)
                {
                    return selectedCustomer;
                }
                AnsiConsole.MarkupLine("[red]Ogiltigt val.[/]");

            }

        }
    }
}
