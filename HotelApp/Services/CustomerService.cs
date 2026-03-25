using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class CustomerService
    {
        public List<Customer> _Customers = new List<Customer>();
        public void SeedCustomers()
        {
            _Customers.Add(new Customer(1, "Henry", "Brandt", new DateTime(1993, 06, 07), true));
            _Customers.Add(new Customer(2, "Hanna", "Verlage", new DateTime(1997, 03, 15), true));
            _Customers.Add(new Customer(3, "Alex", "Araujo", new DateTime(1993, 03, 20), true));
            _Customers.Add(new Customer(4, "Mirza", "Hujic", new DateTime(1992, 02, 26), true));

        }
        public List<Customer> GetCustomers()
        {
            return _Customers;
        }
        
        public void ReadCustomer()
        {
            var activeCustomers = GetCustomers().Where(c => c.IsActive == true).ToList();
            activeCustomers.ForEach(c => c.ShowCustomerData());

            var pressAnyKeyMessage = PressAnyKeyToContinue.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public void RemoveCostumerById ()
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
    }
}
