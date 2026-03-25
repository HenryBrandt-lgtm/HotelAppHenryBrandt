using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class CustomerService : ICrud
    {
        public List<Customer> _Customers = new List<Customer>();
                
        public void Read()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                foreach (var guest in dbContext.Customer)
                {
                    Console.WriteLine($"Namn: {guest.FullName}");
                    Console.WriteLine($"Ålder: {guest.Age}");
                    Console.WriteLine("====================");
                }
            }

                var pressAnyKeyMessage = PressAnyKeyToContinue.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public void Delete ()
        {
            
            var activeCustomer = _Customers.Where(c => c.IsActive == true).ToList();
            var nameOptions = activeCustomer.Select(c => $"{c.FirstName} {c.LastName}").ToList();
            int index = ScrollMenu.ScrollingMenu(nameOptions);

            var customerToRemove = activeCustomer[index];
            customerToRemove.IsActive = false;

            var removedCustomer = new Text($"{customerToRemove.FullName} has been removed.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(removedCustomer);
            var pressAnyKeyMessage = PressAnyKeyToContinue.GetPressAnyKeyText();
            AnsiConsole.WriteLine(); 
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Console.Clear();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
