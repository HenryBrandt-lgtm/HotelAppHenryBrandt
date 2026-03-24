using HotelApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; } 

        public string FirstName {  get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public bool IsActive { get; set; }

        public List<Invoice> Invoices { get; set; } =
            new List<Invoice>();

        public List<Booking> Bookings { get; set; } =
            new List<Booking>();

        public string FullName => $"{FirstName} {LastName}";

        public int Age ()
        {  
            var today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (Birthday.Date > today.AddYears(-age))
            {
                age--;
            }
            return age; 
        }

        public Customer(int id, string firstName, string lastName, DateTime birthday, bool isActive)
        {
            CustomerId = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            IsActive = isActive;
        }
        public void ShowCustomerData()
        {
            Console.WriteLine($"{CustomerId}. Name: {FirstName} {LastName} Age: {Age()}");
        }
    }
}
