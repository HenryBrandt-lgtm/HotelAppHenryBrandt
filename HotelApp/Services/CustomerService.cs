using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class CustomerService (ApplicationDbContext db)
    {
        public bool CustomerExists(string firstName, string lastName)
        {
            return db.Customers.Any(c => c.FirstName == firstName && c.LastName == lastName);
        }
        public List<Customer> GetActiveCustomers() => db.Customers.Where(c => c.IsActive).ToList();

        public List<Customer> GetInactiveCustomers() => db.Customers.Where(c => !c.IsActive).ToList();
        public bool HasActiveBookings(Customer customer) => customer.Bookings?.Any(b => b.IsActive) == true;
        
        public void Delete(Customer customer)
        {
            customer.IsActive = false;
            db.SaveChanges();
        }


        public void CreateCustomer(string firstName, string lastName, DateOnly birthday)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Birthday = birthday,
                IsActive = true
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }


        public void Update(Customer customer)
        {            
            db.SaveChanges();
        }
        
        public void Reactivate(Customer customer)
        {
            customer.IsActive = true;
            db.SaveChanges();
        }
    }
}

