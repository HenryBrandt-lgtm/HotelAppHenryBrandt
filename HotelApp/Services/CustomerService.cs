using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using System.Globalization;

namespace HotelApp.Services
{
    public class CustomerService : ICrud
    {
        public void Read()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            using (var dbContext = new ApplicationDbContext())
            {
                var activeGuests = dbContext.Customer.Where(c => c.IsActive == true);
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
                
            }

            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public void Delete()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.Clear();
                var selectMessage = new Text($"Välj från listan med gäster vem du vill ta bort.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(selectMessage);
                var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;

                var activeCustomers = dbContext.Customer.Where(c => c.IsActive).ToList();
                if (!activeCustomers.Any())
                {
                    AnsiConsole.MarkupLine("[red]Inga gäster att radera.[/]");
                    return;
                }

                var nameOptions = activeCustomers.Select(c => $"{c.FullName} {c.Age()} år").ToList();

                int index = ScrollMenu.ScrollingMenu(nameOptions);

                var customerToRemove = activeCustomers[index];
                customerToRemove.IsActive = false;
                dbContext.SaveChanges();


                var removedCustomer = new Text($"{customerToRemove.FullName} har tagits bort.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(removedCustomer);
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;
                Console.Clear();
            }
        }

        public void Create()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.WriteLine("Skapa en ny person");
                               
                var firstNameInput = VarValidater.GetRequiredString("Ange förnamn: ");

                var lastNameInput = VarValidater.GetRequiredString("Ange efternamn: ");

                var ageInput = VarValidater.GetValidDate("Ange födelseår (yyy-MM-dd): ");


                //foreach (var county in dbContext.County)
                //{
                //    Console.WriteLine($"{county.Id} - {county.Name}");
                //}
                //Console.WriteLine("Ange Id på County");
                //var countyId = Convert.ToInt32(Console.ReadLine());
                //var countyInput = dbContext.County.First(c => c.Id == countyId);

                if (dbContext.Customer.Any(c => c.FirstName == firstNameInput && c.LastName == lastNameInput))
                    Console.WriteLine("Gästen finns redan inlagd. Dubbelkika bland raderade gäster om hen har blivit inaktiverad");
                else
                {
                    dbContext.Customer.Add(new Customer
                    {
                        FirstName = firstNameInput,
                        LastName = lastNameInput,
                        Birthday = ageInput,
                        IsActive = true
                    });
                    dbContext.SaveChanges();
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
            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            using (var dbContext = new ApplicationDbContext())
            {
                var activeCustomers = dbContext.Customer.Where(c => c.IsActive).ToList();

                var nameOptions = activeCustomers.Select(c => $"Namn: {c.FullName} person.nr: {c.Birthday}").ToList();

                int index = ScrollMenu.ScrollingMenu(nameOptions);

                var customerToUpdate = activeCustomers[index];
                var updateOptions = new List<string> { "FirstName", "LastName", "Birthday" };

                int updateIndex = ScrollMenu.ScrollingMenu(updateOptions);

                switch (updateIndex)
                {
                    case 0:
                        customerToUpdate.FirstName = VarValidater.GetRequiredString("Nytt förnamn: ");
                        break;

                    case 1:
                        customerToUpdate.LastName = VarValidater.GetRequiredString("Nytt efternamn: ");
                        break;

                    case 2:
                        customerToUpdate.Birthday = VarValidater.GetValidDate("Ny födelsedag: ");
                        break;
                }

                dbContext.SaveChanges();
            }
        }

        public void ReadDeleted()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                foreach (var guest in dbContext.Customer.Where(c => c.IsActive == false))
                {
                    var content = new Markup($"[yellow]Namn:[/] {guest.FullName}" +
                        $"\n [blue]Ålder:[/] {guest.Age()}" +
                        $"\n ====================");

                    AnsiConsole.Write(Align.Center(content));
                }
            }

            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Console.Clear();

        }
        public void Reactivate()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.Clear();
                var selectMessage = new Text($"Välj från listan med gäster vem du vill ta tillbaka.", new Style(Color.Cyan))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(selectMessage);
                var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;

                var activeCustomers = dbContext.Customer.Where(c => !c.IsActive).ToList();

                var nameOptions = activeCustomers.Select(c => $"{c.FullName} {c.Age()} år").ToList();

                int index = ScrollMenu.ScrollingMenu(nameOptions);

                var returningCustomer = activeCustomers[index];
                returningCustomer.IsActive = true;
                dbContext.SaveChanges();


                var reactivatedCustomer = new Text($"{returningCustomer.FullName} har återaktiverats.", new Style(Color.Green))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(reactivatedCustomer);
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;
                Console.Clear();
            }
        }
    }
}
